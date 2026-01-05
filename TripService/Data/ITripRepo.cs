using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TripService.Models;

namespace TripService.Data
{
    public interface ITripRepo
    {
        bool SaveChanges();

        // Member related methods
        IEnumerable<Member> GetAllMembers();
        void CreateMember(Member member);
        bool MemberExists(string memberId);


        // Trip related methods
        IEnumerable<Trip> GetAllTripsForMember(string memberId);
        Trip GetTripForMember(string memberId, int tripId);
        void CreateTrip(string memberId, Trip trip);
    }
}