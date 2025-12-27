using MemberService.Models;

namespace MemberService.Data
{
    public static class PrepareDb
    {
        /// <summary>
        /// Prepares the database by seeding initial data if necessary.
        /// </summary>
        /// <param name="context"></param>
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            Console.WriteLine("in PrepPopulation...");
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
            // SeedData(context);
        }

        /// <summary>
        /// Seeds the database with initial member data if the Members table is empty.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void SeedData(AppDbContext? context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Members.Any())
            {
                Console.WriteLine("Seeding Data...");

                context.Members.AddRange(
                    new Member() { MemberId = "M001", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", HealthPlanName = "Anthem" },
                    new Member() { MemberId = "M002", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "555-0123", HealthPlanName = "Kaiser" },
                    new Member() { MemberId = "M003", FirstName = "Josh", LastName = "Kim", Email = "josh.kim@example.com", Phone = "511-0124", HealthPlanName = "Anthem" },
                    new Member() { MemberId = "M004", FirstName = "Amy", LastName = "Byers", Email = "amy.byers@example.com", Phone = "522-0125", HealthPlanName = "Aetna" }
                );
                context.SaveChanges();
            }
            else 
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}