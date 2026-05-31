using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities.Product
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        //public string UserId { get; set; } = null!;   // one rating per user per product
        public byte Stars { get; set; }               // 1–5
        public string? Review { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; } = null!;
    }
}
