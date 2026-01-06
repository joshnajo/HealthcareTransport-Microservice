using MemberService.Dtos;

namespace MemberService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewMember(MemberPublishedDto memberPublishedDto);

    }
}