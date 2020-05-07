using Microsoft.EntityFrameworkCore;
using RN.Domain.Entities;
using RN.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        //DbSet<T> Set<T>() where T : class;
        DbSet<Note> Notes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
