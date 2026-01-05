using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TripService.Data;
using TripService.Dtos;

namespace TripService.Controllers
{
    [Route("api/t/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ITripRepo _tripRepo;
        private readonly IMapper _mapper;

        public MembersController(ITripRepo tripRepo, IMapper mapper)
        {
            _tripRepo = tripRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDto>> GetAllMembers()
        {
            Console.WriteLine("Getting all members from TripService");
            var _members = _tripRepo.GetAllMembers();
            if(_members == null || !_members.Any())
            {
                return NotFound("No members found");
            }

            // Map members to MemberReadDto and return the result
            return Ok(_mapper.Map<IEnumerable<MemberReadDto>>(_members));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("Inbound POST connection to TripService successful.");

            return Ok("Inbound test connection from TripService");
        }
    }
}