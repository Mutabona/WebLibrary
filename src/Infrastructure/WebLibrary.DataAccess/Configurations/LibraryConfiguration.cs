using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.DataAccess.Configurations
{
    /// <summary>
    /// Файл конфигурации сущности библиотеки.
    /// </summary>
    internal class LibraryConfiguration : IEntityTypeConfiguration<Domain.Library.Entity.Library>
    {
        public void Configure(EntityTypeBuilder<Domain.Library.Entity.Library> builder)
        {
            builder
                .ToTable("Libraries")
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .HasMaxLength(128);

            builder
                .Property(e => e.IsPublic);

            builder
                .Property(e => e.FilesAmount)
                .HasMaxLength(128);

            builder
                .Property(e => e.CreatedAt)
                .HasMaxLength(128);

            builder
                .Property(e => e.OwnerName)
                .HasMaxLength(128);

            builder
                .Property(e => e.FilesAmount)
                .HasMaxLength(128);
        }
    }
}
