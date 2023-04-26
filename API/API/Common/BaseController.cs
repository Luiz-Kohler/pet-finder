using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Common
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected int GetCurrentUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Convert.ToInt16(identity.Claims.FirstOrDefault(x => x.Type == "id").Value);
        }
    }
}
