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
    /// Файл конфигурации сущности файла.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Users.Entity.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Users.Entity.User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .Property(x => x.Login)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .Property(x => x.MiddleName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.CreatedAt)
                .IsRequired();
                //.IsRowVersion();
        }
    }
}
