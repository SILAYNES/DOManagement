using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DOManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator {
            get {
                if (_mediator == null)
                {
                    _mediator = HttpContext.RequestServices.GetService<ISender>();
                }
                return _mediator;
            }
        }
    }
}