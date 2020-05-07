using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RN.Infrastructure.Persistence.Configurations
{
    public class NoteConfigurations : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired();

            // TODO: here too
        }
    }
}
