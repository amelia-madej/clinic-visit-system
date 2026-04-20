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
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("Medications");
            builder.HasKey(m => m.MedicationId);
            builder.Property(m => m.MedicationId)
                   .ValueGeneratedOnAdd();
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.DosageForm)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Form)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(m => m.StrengthValue)
                .IsRequired()
                .HasPrecision(10, 2);
            builder.Property(m => m.StrengthUnit)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(m => m.Manufacturer)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(m => m.Packaging)
                .IsRequired()
                .HasMaxLength(500);            
            builder.Property(m => m.ActiveIngredient)
                .IsRequired()
                .HasMaxLength(300);

            //Relations
            builder.HasMany(m => m.PrescriptionItems)
                   .WithOne(pi => pi.Medication)
                   .HasForeignKey(pi => pi.MedicationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
