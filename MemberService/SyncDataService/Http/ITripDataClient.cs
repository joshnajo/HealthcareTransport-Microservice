using MemberService.Dtos;

namespace MemberService.SyncDataService.Http
{
    public interface ITripDataClient
    {
        Task SendMemberToTrip(MemberReadDto member);
    }
}