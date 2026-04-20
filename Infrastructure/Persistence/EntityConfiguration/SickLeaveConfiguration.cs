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
    public class SickLeaveConfiguration : IEntityTypeConfiguration<SickLeave>
    {
        public void Configure(EntityTypeBuilder<SickLeave> builder)
        {
            builder.ToTable("SickLeaves");
            builder.HasKey(sl => sl.SickLeaveId);
            builder.Property(sl => sl.SickLeaveId)
                    .ValueGeneratedOnAdd();
            builder.Property(sl => sl.StartDate)
                    .IsRequired();
            builder.Property(sl => sl.EndDate)
                    .IsRequired();
            builder.Property(sl => sl.Reason)
                    .HasMaxLength(500);
            builder.Property(v => v.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAdd();
        }
    }
}
