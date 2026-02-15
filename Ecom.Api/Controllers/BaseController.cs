using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Reposities;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       protected readonly IUnitOfWork work;
        protected readonly IMapper mapper;

        public BaseController(IUnitOfWork work)
        {
            this.work = work;
        }

        public BaseController(IUnitOfWork work, IMapper mapper) 
       {
             this.work = work;
             this.mapper = mapper;
       }
    }
}
