using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities.Service
{
    public class RatingService : IRatingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RatingService(IMapper mapper, IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RatingSummaryDTO> GetByProductAsync(int productId)
        {
            var ratings = await _unitOfWork.RatingRepository.GetByProductIdAsync(productId);
            var list = ratings.ToList();

            return new RatingSummaryDTO
            {
                AverageStars = await _unitOfWork.RatingRepository.GetAverageStarsAsync(productId),
                TotalRatings = await _unitOfWork.RatingRepository.GetTotalRatingsAsync(productId),
                Ratings = _mapper.Map<IEnumerable<RatingToReturnDTO>>(list)
            };
        }

        public async Task<bool> DeleteAsync(int ratingId)
        {
            var entity = await _unitOfWork.RatingRepository.GetByIdAsync(ratingId);

            if (entity is null) return false;

            await _unitOfWork.RatingRepository.DeleteAsync(entity.Id);

            return true;
        }


        public async Task<RatingToReturnDTO> UpdateAsync(int ratingId, CreateRatingDTO dto)
        {
            var entity = await _unitOfWork.RatingRepository.GetByIdAsync(ratingId);

            if (entity is null)
                throw new KeyNotFoundException($"Rating {ratingId} not found");

            entity.Stars = dto.Stars;
            entity.Review = dto.Review;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.RatingRepository.UpdateAsync(entity); 
            return _mapper.Map<RatingToReturnDTO>(entity);
        }



        public async Task<RatingToReturnDTO> AddAsync(int productId, CreateRatingDTO dto)
        {
            var entity = _mapper.Map<ProductRating>(dto);
            entity.ProductId = productId;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.RatingRepository.AddAsync(entity);

            return _mapper.Map<RatingToReturnDTO>(entity);
        }

    
    }
}
