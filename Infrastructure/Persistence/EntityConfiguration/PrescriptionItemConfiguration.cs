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
    public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            builder.ToTable("PrescriptionItems");
            builder.HasKey(pi => pi.PrescriptionItemId);
            builder.Property(p => p.PrescriptionItemId)
                   .ValueGeneratedOnAdd();
            builder.Property(pi => pi.Dosage)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(pi => pi.Quantity)
                   .IsRequired();
            builder.Property(pi => pi.Instructions)
                   .IsRequired()
                   .HasMaxLength(500);
        }
    }
}
