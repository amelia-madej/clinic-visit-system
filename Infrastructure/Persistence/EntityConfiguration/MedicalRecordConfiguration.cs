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
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecords");
            builder.HasKey(mr => mr.MedicalRecordId);
            builder.Property(mr => mr.Diagnosis)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(mr => mr.Recommendations)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(mr => mr.Interview)
                .HasMaxLength(500);
            builder.Property(v => v.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAdd();

            //Relations
            builder.HasOne(u => u.SickLeave)
                   .WithOne(d => d.MedicalRecord)
                   .HasForeignKey<SickLeave>(d => d.MedicalRecordId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Prescriptions)
                   .WithOne(d => d.MedicalRecord)
                   .HasForeignKey(d => d.MedicalRecordId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
