using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    /// <summary>
    /// Get Mediator 
    /// </summary>
    public IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
}
