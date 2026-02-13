using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.price).HasColumnType("decimal(18,2)");

        }
    }
}
