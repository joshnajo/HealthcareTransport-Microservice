using TripService.Models;
using TripService.SyncDataServices.Grpc;

namespace TripService.Data
{
    public static class PrepDb
    {
        public static void PrepMember(IApplicationBuilder appBuilder)
        {
            using(var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IMemberDataClient>();
                if (grpcClient == null)
                {
                    Console.WriteLine("--> gRPC client not registered");
                    return;
                }
                var members = grpcClient.ReturnAllMembers();
                if (members == null)
                {
                    Console.WriteLine("--> No members returned from gRPC");
                    return;
                }
                var tripRepo = serviceScope.ServiceProvider.GetService<ITripRepo>();
                if (tripRepo == null)
                {
                    Console.WriteLine("--> TripRepo not available");
                    return;
                }
                else
                {
                    SeedData(tripRepo, members);
                }
            }
        }

        private static void SeedData(ITripRepo repo, IEnumerable<Member> members)
        {
            Console.WriteLine($"Seeding new Platforms...");
            foreach(var member in members)
            {
                if(!repo.ExternalMemberExists(member.ExternalRefId))
                {
                    repo.CreateMember(member);
                }
                repo.SaveChanges();
            }
        }
    }
}