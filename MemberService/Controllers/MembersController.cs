using AutoMapper;
using MemberService.Data;
using MemberService.Dtos;
using MemberService.Models;
using MemberService.SyncDataService.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.Controllers
{
    //api/members
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepo _memberRepo;
        private readonly IMapper _mapper;
        private readonly ITripDataClient _tripDataClient;

        public MembersController(IMemberRepo memberRepo, IMapper mapper, ITripDataClient tripDataClient)
        {
            _memberRepo = memberRepo;
            _mapper = mapper;
            _tripDataClient = tripDataClient;
        }

        // GET api/members
        // http://localhost:5279/api/members
        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDto>> GetAllMembers()
        {
            Console.WriteLine("Getting Members");
            var members = _memberRepo.GetAllMembers();
            // uses this mapping CreateMap<Member,MemberReadDto>() in MembersProfile.cs
            var memberDTOs = _mapper.Map<IEnumerable<MemberReadDto>>(members);
            return Ok(memberDTOs);
        }

        // GET api/members/{id}
        // http://localhost:5279/api/members/2
        [HttpGet("{id}", Name = "GetMemberById")]
        public ActionResult<MemberReadDto> GetMemberById(int id)
        {
            var member = _memberRepo.GetMemberById(id);
            if(member == null)
            {
                return NotFound();
            }
            // uses this mapping CreateMap<Member,MemberReadDto>() in MembersProfile.cs
            var memberDTO = _mapper.Map<MemberReadDto>(member);
            return Ok(memberDTO);
        }

        // POST api/members
        [HttpPost]
        public async Task<ActionResult<MemberReadDto>> CreateMember(MemberCreateDto memberToCreateDto)
        {
            // uses this mapping CreateMap<MemberCreateDto,Member>() in MembersProfile.cs
            Console.WriteLine("Creating member...");
            
            var member = _mapper.Map<Member>(memberToCreateDto);
            
            _memberRepo.CreateMember(member);
            _memberRepo.SaveChanges();

            var memberReadDto = _mapper.Map<MemberReadDto>(member);

            // Send member data to TripService
            try
            {
               await _tripDataClient.SendMemberToTrip(memberReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously to TripService: {ex.Message}");
            }
            
            // Returns a 201 response with a Location header pointing to the newly created resource
            // GetMemberById is the name of the route defined in the HttpGet attribute above 
            return CreatedAtRoute(
                nameof(GetMemberById), 
                new { Id = memberReadDto.Id }, 
                memberReadDto
            );
        }
    }
}