using Ecom.Core.Entities.Order;
using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.config
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.shippingAddress,
               n => { n.WithOwner(); });

            builder.HasMany(x => x.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.status).HasConversion(o => o.ToString(),
               o => (Status)Enum.Parse(typeof(Status), o));

            builder.Property(m => m.SubTotal).HasColumnType("decimal(18,2)");

        }

    }
}
