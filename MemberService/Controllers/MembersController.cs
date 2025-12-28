using AutoMapper;
using MemberService.Data;
using MemberService.Dtos;
using MemberService.Models;
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

        public MembersController(IMemberRepo memberRepo, IMapper mapper)
        {
            _memberRepo = memberRepo;
            _mapper = mapper;
        }

        // GET api/members
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
        [HttpGet("{id}")]
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

        public ActionResult<MemberReadDto> CreateMember(MemberCreateDto memberToCreateDto)
        {
            // uses this mapping CreateMap<MemberCreateDto,Member>() in MembersProfile.cs
            Console.WriteLine("Creating member...");
            
            var member = _mapper.Map<Member>(memberToCreateDto);
            
            _memberRepo.CreateMember(member);
            _memberRepo.SaveChanges();

            var memberReadDto = _mapper.Map<MemberReadDto>(member);

            return CreatedAtRoute(
                nameof(GetMemberById), 
                new { Id = memberReadDto.Id }, 
                memberReadDto
            );
        }
    }
}