using System.Diagnostics.Eventing.Reader;
using TripService.Models;

namespace TripService.Data
{
    public class TripRepo : ITripRepo
    {
        private readonly AppDbContext _context;
        public TripRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        # region Member related methods

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        /// <summary>
        /// Create a new member
        /// </summary>
        /// <param name="member"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CreateMember(Member member)
        {
            if(member ==null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Add(member);
        }

        /// <summary>
        /// Check if a member exists by memberId
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public bool MemberExists(string memberId)
        {
            return _context.Members.Any(m => m.MemberId.Equals(memberId));
        }

        /// <summary>
        /// Check if member exists based on external ref Id
        /// </summary>
        /// <param name="externalMemberId"></param>
        /// <returns></returns>
        public bool ExternalMemberExists(int externalMemberId)
        {
            return _context.Members.Any(m => m.ExternalRefId == externalMemberId);
        }

        #endregion

        #region Trip related methods

        /// <summary>
        /// Get all trips for a specific member
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Trip> GetAllTripsForMember(string memberId)
        {
            return _context.Trips
                    .Where(t => t.MemberId.Equals(memberId))
                    .OrderBy(t => t.TripDate);
        }

        /// <summary>
        /// Get a specific trip by tripId for a specific member
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="tripId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Trip GetTripForMember(string memberId, int tripId)
        {
            if(!MemberExists(memberId))
            {
                throw new ArgumentException("Member does not exist", nameof(memberId));
            }
            else
            {
                return _context.Trips
                    .FirstOrDefault(t => t.MemberId.Equals(memberId) && t.Id == tripId)!;
            }

        }

        /// <summary>
        /// Create a new trip for a specific member
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="trip"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CreateTrip(string memberId, Trip trip)
        {
            if(trip == null)
            {
                throw new ArgumentNullException(nameof(trip));
            }

            trip.MemberId = memberId;
            _context.Trips.Add(trip);

            // else
            // {
            //     var member = _context.Members
            //         .FirstOrDefault(m => m.MemberId.Equals(memberId));
                
            //     if(member == null)
            //     {
            //         throw new ArgumentNullException(nameof(member));
            //     }
            //     else
            //     {
            //         trip.MemberId = member.MemberId;
            //         _context.Trips.Add(trip);
            //     }
            // }     
        }
        #endregion


    }
}