using Ecom.Api.Controllers;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Reposities;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/products/{productId:int}/ratings")]
public class RatingController : BaseController
{
    private readonly IRatingService _ratingService;

    public RatingController(IUnitOfWork work, IRatingService ratingService) : base(work)
    {
        _ratingService = ratingService;
    }

    // GET api/v1/products/5/ratings
    [HttpGet]
    [ProducesResponseType(typeof(RatingSummaryDTO), 200)]
    [ProducesResponseType(typeof(ResponseAPI), 404)]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var result = await _ratingService.GetByProductAsync(productId);
        return Ok(result);
    }

    // POST api/v1/products/5/ratings
    [HttpPost]
    [ProducesResponseType(typeof(RatingToReturnDTO), 201)]
    [ProducesResponseType(typeof(ResponseAPI), 400)]
    public async Task<IActionResult> Add(int productId, [FromBody] CreateRatingDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseAPI(400));

        var result = await _ratingService.AddAsync(productId, dto);
        return CreatedAtAction(nameof(GetByProduct), new { productId }, result);
    }

    // PUT api/v1/products/5/ratings/3
    [HttpPut("{ratingId:int}")]
    [ProducesResponseType(typeof(RatingToReturnDTO), 200)]
    [ProducesResponseType(typeof(ResponseAPI), 404)]
    public async Task<IActionResult> Update(int productId, int ratingId, [FromBody] CreateRatingDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseAPI(400));

        try
        {
            var result = await _ratingService.UpdateAsync(ratingId, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ResponseAPI(404, $"Rating {ratingId} not found"));
        }
    }

    // DELETE api/v1/products/5/ratings/3
    [HttpDelete("{ratingId:int}")]
    [ProducesResponseType(typeof(ResponseAPI), 200)]
    [ProducesResponseType(typeof(ResponseAPI), 404)]
    
    public async Task<IActionResult> Delete(int productId, int ratingId)
    {
        var deleted = await _ratingService.DeleteAsync(ratingId);

        return deleted
            ? Ok(new ResponseAPI(200, "Rating deleted successfully"))
            : NotFound(new ResponseAPI(404, $"Rating {ratingId} not found"));
    }
}
