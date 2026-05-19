using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                   .ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(u => u.Password)
                   .IsRequired();
            builder.Property(u => u.PhotoDataUrl);
            builder.Property(u => u.Role)
                   .IsRequired();

            //Relations
            builder.HasOne(u => u.Patient)
                   .WithOne(p => p.User)
                   .HasForeignKey<Patient>(p => p.UserId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Doctor)
                   .WithOne(d => d.User)
                   .HasForeignKey<Doctor>(d => d.UserId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
