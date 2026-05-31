using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.config
{
    public class ProductRatingsConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            //pk
            builder.HasKey(x => x.Id);
            //star
            builder.Property(x => x.Stars)
                  .HasColumnType("tinyint")
                  .IsRequired(); ;

            builder.Property(x => x.Review)
            .HasMaxLength(1000)
            .IsRequired(false);

            builder.Property(x => x.UpdatedAt)
          .IsRequired()
          .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(x => x.Product)
           .WithMany(p => p.Ratings)
           .HasForeignKey(x => x.ProductId)
           .OnDelete(DeleteBehavior.Cascade);

            //index 
            builder.HasIndex(x => x.ProductId)
            .HasDatabaseName("IX_ProductRating_ProductId");


        }

    }
}
