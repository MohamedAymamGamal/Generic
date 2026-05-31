using Ecom.Api.Sharing;
using Ecom.Core.DTO;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Service
{
    public interface IRatingService
    {
        Task<RatingSummaryDTO> GetByProductAsync(int productId);
        Task<RatingToReturnDTO> AddAsync(int productId, CreateRatingDTO dto);
        Task<RatingToReturnDTO> UpdateAsync(int ratingId, CreateRatingDTO dto);
        Task<bool> DeleteAsync(int ratingId);
    }
}
