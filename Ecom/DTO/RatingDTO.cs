using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecom.Core.DTO
{
    public record CreateRatingDTO
    {
        [Range(1, 5, ErrorMessage = "Stars must be between 1 and 5")]
        public byte Stars { get; init; }

        [MaxLength(1000, ErrorMessage = "Review cannot exceed 1000 characters")]
        public string? Review { get; init; }
    }
    public record UpdateRatingDTO : CreateRatingDTO
    {
        public int Id { get; set; }
    }

    public record RatingToReturnDTO
    {
        public int Id { get; init; }
        public int ProductId { get; init; }
        public byte Stars { get; init; }
        public string? Review { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }

    public record RatingSummaryDTO
    {
        public double AverageStars { get; init; }
        public int TotalRatings { get; init; }
        public IEnumerable<RatingToReturnDTO> Ratings { get; init; } = [];
    }
}


