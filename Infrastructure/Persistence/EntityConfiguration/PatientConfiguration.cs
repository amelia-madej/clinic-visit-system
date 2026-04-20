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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasKey(p => p.PatientId);
            builder.Property(p => p.PatientId)
                   .ValueGeneratedOnAdd();
            builder.Property(p => p.PESEL)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.DateOfBirth)
                   .IsRequired();
            builder.Property(p => p.Gender)
                   .IsRequired();            
        }
    }
}
