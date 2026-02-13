using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Data.config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(x => x.Id)
                 .IsRequired()
                 .HasMaxLength(30);
            builder.Property(x => x.ImageName)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(x => x.Product)
                 .WithMany(p => p.Photos)
                 .HasForeignKey(x => x.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
