using AutoMapper;
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
        }
    }
}