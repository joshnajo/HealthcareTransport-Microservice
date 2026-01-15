using TripService.Models;

namespace TripService.SyncDataServices.Grpc
{
    public interface IMemberDataClient
    {
        IEnumerable<Member> ReturnAllMembers();
    }
}