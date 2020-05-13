using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RN.Application.Common.Interfaces;
using RN.Domain.Common;
using RN.Domain.Entities;
using RN.Domain.ValueObjects;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        private readonly ICurrentUserService currentUserService;
        private readonly IDateTime dateTimeService;
        private readonly IPasswordHasher passwordHasher;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTimeService,
            IPasswordHasher passwordHasher
        ) : base(options)
        {
            this.currentUserService = currentUserService;
            this.dateTimeService = dateTimeService;
            this.passwordHasher = passwordHasher;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUserService.UserId;
                        entry.Entity.Created = dateTimeService.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = currentUserService.UserId;
                        entry.Entity.LastModified = dateTimeService.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public void SetModifiedState(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // TODO: Create correct connection string => configuration.GetConnectionString("DefaultConnection")
            // options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RN.DB;Trusted_Connection=True;MultipleActiveResultSets=true");
            // options.UseMySql("Server=localhost;Database=RN.DB;User=root;Password=password;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedDefaultData(builder);
            base.OnModelCreating(builder);
        }

        private void SeedDefaultData(ModelBuilder builder)
        {
            var defaultUserId = Guid.NewGuid().ToString();

            builder.Entity<User>().HasData(new User
            {
                Id = defaultUserId,
                Name = "admin",
                Password = passwordHasher.EncodePassword("admin"),
                Surname = "admin",
                Email = "admin@admin.admin"
            });

            builder.Entity<Role>().HasData(new Role[] {
                new Role { Id = 1, Name = Domain.Roles.User },
                new Role { Id = 2, Name = Domain.Roles.Admin }
            });

            builder.Entity<UserRole>().HasData(new { Id = 1, UserId = defaultUserId, RoleId = 1 });
            builder.Entity<UserRole>().HasData(new { Id = 2, UserId = defaultUserId, RoleId = 2 });
        }
    }
}
