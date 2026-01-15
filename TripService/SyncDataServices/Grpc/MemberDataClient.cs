using AutoMapper;
using Grpc.Net.Client;
using MemberService;
using TripService.Models;

namespace TripService.SyncDataServices.Grpc
{
    public class MemberDataClient : IMemberDataClient
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public MemberDataClient(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public IEnumerable<Member> ReturnAllMembers()
        {
            var grpcUrl = _config["GrpcMember"];
            if (grpcUrl == null)
            {
                Console.WriteLine("--> grpcUrl is null");
                return null;
            }
            Console.WriteLine($"Calling GRPC Service {_config["GrpcMember"]}");
            var channel = GrpcChannel.ForAddress( grpcUrl, new GrpcChannelOptions
            {
                HttpHandler = new HttpClientHandler()
            });
            
            var client = new GrpcMember.GrpcMemberClient(channel);
            var request = new GetAllRequest();
            try
            {
                var result = client.GetAllMembers(request);
                if (result == null)
                {
                    Console.WriteLine("--> gRPC response was null");
                    return null;
                }
                return _mapper.Map<IEnumerable<Member>>(result.Member);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Couldn't call GRPC Service {ex.Message}");
                throw null;
            }


            // throw new System.NotImplementedException();
        }
    }
}