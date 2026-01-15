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
            // trigger for our event will be in our controller in Member Create Action     
            CreateMap<MemberReadDto, MemberPublishedDto>();
            CreateMap<Member, GrpcMemberModel>()
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}