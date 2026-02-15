using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Reposities;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.v1
{

    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork work, IMapper mapper) : base(work,mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest();
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }
        }


        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }
        }
        [HttpPost("add-category")]

        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO category)
        {
            try
            {
                var Category = mapper.Map<Category>(category);
                await work.CategoryRepository.AddAsync(Category);
                return Ok("Item has been added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("update-category")]

        public async Task<IActionResult> UpdateCategory(CategoryUpdateDTO category)
        {
            try
            {
                var Category = new Category()
                {
                    Description = category.Description,
                    Name = category.Name,
                    Id = category.id
                };
                await work.CategoryRepository.UpdateAsync(Category);
                return Ok("Item has been updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                return Ok("Item has been deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
