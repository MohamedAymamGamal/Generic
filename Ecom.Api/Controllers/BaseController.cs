using Ecom.Core.Interfaces;
using Ecom.infrastructure.Reposities;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
       protected readonly IUnitOfWork work;
       public BaseController(IUnitOfWork work) 
       {
             this.work = work;
       }
    }
}
