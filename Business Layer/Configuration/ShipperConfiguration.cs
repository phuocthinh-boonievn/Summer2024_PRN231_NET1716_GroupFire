using Data_Layer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Configuration
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable("Shipper");
            builder.HasKey(x => x.ShipperId);
            builder.Property(x => x.ShipperStatus).HasMaxLength(128);

            builder.HasOne(x => x.User).WithMany(x => x.Shippers).HasForeignKey(x => x.userId);
        }
    }
}
