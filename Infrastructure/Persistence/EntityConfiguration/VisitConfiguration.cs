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
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visits");
            builder.HasKey(v => v.VisitId);
            builder.Property(v => v.VisitId)
                   .ValueGeneratedOnAdd();
            builder.Property(v => v.VisitDateTime)
                   .IsRequired();
            builder.Property(v => v.Status)
                   .IsRequired();
            builder.Property(v => v.VisitType)
                   .IsRequired();
            builder.Property(v => v.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAdd();

            //Relations
            builder.HasOne(v => v.Patient)
                   .WithMany(p => p.Visits)
                   .HasForeignKey(v => v.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(v => v.Doctor)
                   .WithMany(d => d.Visits)
                   .HasForeignKey(v => v.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
