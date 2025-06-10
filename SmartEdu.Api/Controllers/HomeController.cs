using Microsoft.AspNetCore.Mvc;

namespace SmartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult<string> Get() =>
            Ok("Welcome to SmartEdu API!");
    }
}
