using System.Text.Json;
using AutoMapper;
using TripService.Data;
using TripService.Dtos;
using TripService.Models;

namespace TripService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, 
                IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// process and add event to database
        /// </summary>
        /// <param name="message"></param>
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch(eventType)
            {
                case EventType.MemberPublished:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// get the event type if passed
        /// </summary>
        /// <param name="notificationMessage"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("Determining the Event");
            
            //Deserialize the event
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            string strEvent = eventType?.Event ?? throw new NullReferenceException();
            switch(strEvent)
            {
                case "Member_Published":
                    Console.WriteLine("Member Published Event Detected");
                    return EventType.MemberPublished;
                default :
                    Console.WriteLine("Couldn't determine Event Type");
                    return EventType.Undetermined;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberPublishedMessage"></param>
        private void Addmember(string memberPublishedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var tripRepo = scope.ServiceProvider.GetRequiredService<ITripRepo>();

                //Deserialize member published dto to member
                var memberPublishedDto = JsonSerializer.Deserialize<MemberPublishedDto>(memberPublishedMessage);
                try
                {
                    var member = _mapper.Map<Member>(memberPublishedDto);
                    if(!tripRepo.ExternalMemberExists(member.ExternalRefId))
                    {
                        tripRepo.CreateMember(member);
                        tripRepo.SaveChanges();
                    }
                    else
                        Console.WriteLine($"Member {member.MemberId} already exists");
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Couldn't add  member to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        MemberPublished,
        Undetermined
    }
}