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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            builder.HasKey(d => d.DoctorId);
            builder.Property(d => d.DoctorId)
                   .ValueGeneratedOnAdd();
            builder.Property(d => d.Specialization)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(d => d.LicenseNumber)
                   .IsRequired()
                   .HasMaxLength(30);
            builder.Property(d => d.Gender)
                   .IsRequired();
        }
    }
}
