using AutoMapper;
using MemberService;
using Microsoft.Extensions.Options;
using TripService.Dtos;
using TripService.Models;

namespace TripService.Profiles
{
    public class TripsProfile : Profile
    {
        public TripsProfile()
        {
            // source -> target
            CreateMap<Member, MemberReadDto>();
            CreateMap<Trip, TripReadDto>();
            CreateMap<TripCreateDto, Trip>();
            //Map ExternalId to Id
            CreateMap<MemberPublishedDto, Member>()
                .ForMember(dest => dest.ExternalRefId, options => options.MapFrom(src => src.Id));
            CreateMap<GrpcMemberModel, Member>()
                .ForMember(dest => dest.ExternalRefId, opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Trips, opt => opt.Ignore());
        }
    }
}