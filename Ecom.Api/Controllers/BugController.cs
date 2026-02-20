using AutoMapper;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var thing = await work.CategoryRepository.GetByIdAsync(100);
            if (thing == null) return NotFound();
            return Ok(thing);

        }

        [HttpGet("server-error")]

        public async Task<ActionResult> ServerError()
        {
            var thing = await work.CategoryRepository.GetByIdAsync(100);
            thing.Name = "";
            return Ok(thing);

        }

        [HttpGet("bad-request/{id}")]
        public async Task<ActionResult> GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}