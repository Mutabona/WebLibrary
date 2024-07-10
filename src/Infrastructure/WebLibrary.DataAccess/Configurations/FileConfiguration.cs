using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary.Domain.Files.Entity;

namespace WebLibrary.DataAccess.Configurations
{
    /// <summary>
    /// Файл конфиггурации сущности файла
    /// </summary>
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Files.Entity.File>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Domain.Files.Entity.File> builder)
        {
            builder.
                ToTable("Files")
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(t => t.ContentType)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(t => t.LibraryId)
                .IsRequired();
        }
    }
}
