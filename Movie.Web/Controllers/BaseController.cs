using Microsoft.AspNetCore.Mvc;

namespace Movie.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
