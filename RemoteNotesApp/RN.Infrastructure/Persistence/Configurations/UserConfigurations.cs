using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RN.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();
            
            // TODO: implement other props
        }
    }
}
