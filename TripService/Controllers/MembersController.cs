using Microsoft.AspNetCore.Mvc;

namespace TripService.Controllers
{
    [Route("api/t/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        public MembersController()
        {
            
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("Inbound POST connection to TripService successful.");

            return Ok("Inbound test connection from TripService");
        }
    }
}