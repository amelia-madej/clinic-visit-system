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
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");
            builder.HasKey(p => p.PrescriptionId);
            builder.Property(p => p.PrescriptionId)
                    .ValueGeneratedOnAdd();
            builder.Property(p => p.ValidUntil)
                    .IsRequired();
            builder.Property(v => v.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAdd();

            //Relations
            builder.HasMany(u => u.Items)
                   .WithOne(d => d.Prescription)
                   .HasForeignKey(d => d.PrescriptionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
