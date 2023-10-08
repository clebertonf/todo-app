using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/teste")]
        public string Get()
        {
            return "Hello Word!";
        }
    }
}
