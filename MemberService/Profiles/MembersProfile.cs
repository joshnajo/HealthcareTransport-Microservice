using AutoMapper;
using Member = MemberService.Models.Member;
using MemberService.Dtos;

namespace  MemberService.Profiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            // Source -> Target

            //source is my model to target is my Member DTO
            // No need to manually map properties with the same names; AutoMapper handles that automatically.
            CreateMap<Member,MemberReadDto>();
            // Here source is MemberCreateDto and target is Member model
            // user enters member data which member create dto captures and maps to member model
            CreateMap<MemberCreateDto,Member>();

        }
    }
}