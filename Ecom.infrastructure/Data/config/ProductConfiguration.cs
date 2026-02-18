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
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");

            builder.HasData(
                     new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop for work and play.", NewPrice = 999.99m, OldPrice= 11.11m, CategoryId = 1 },
                     new Product { Id = 2, Name = "Smartphone", Description = "A sleek smartphone with a stunning display.", NewPrice = 499.99m, OldPrice = 11.11m, CategoryId = 1 },
                     new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones for immersive sound.", NewPrice = 199.99m, OldPrice = 11.11m, CategoryId = 1 }
                 );

        }
    }
}
