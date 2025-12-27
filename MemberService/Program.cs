using MemberService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Configuring the DbContext to use an in-memory database for development and testing purposes
        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseInMemoryDatabase("InMemoryDb"));

        // Registering the repository for dependency injection
        builder.Services.AddScoped<IMemberRepo, MemberRepo>();

        // Adding controllers
        builder.Services.AddControllers();

        // Adding AutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Console.WriteLine("in program.cs...");

        // Seed the in-memory database
         PrepareDb.PrepPopulation(app); // Call the seeding method

        // using (var scope = app.Services.CreateScope())
        // {
        //     Console.WriteLine("in program.cs.. scope...");

        //     var services = scope.ServiceProvider;
        //     try
        //     {
        //         var context = services.GetRequiredService<AppDbContext>();
        //         PrepareDb.SeedData(context); // Call the seeding method
        //     }
        //     catch (Exception ex)
        //     {
        //         var logger = services.GetRequiredService<ILogger<Program>>();
        //         logger.LogError(ex, "An error occurred while seeding the in-memory database.");
        //     }
        // }

        // // Prepare and seed the database with initial data
        // // 3. Ensure the database is created (tables are generated)
        // using (var scope = app.Services.CreateScope())
        // {
        //     var services = scope.ServiceProvider;
        //     var context = services.GetRequiredService<AppDbContext>();
        //     context.Database.EnsureCreated();
        //     // Now call your population logic within this scope or an application scope that uses the same configuration
        //     PrepareDb.PrepPopulation(context); // Pass the context or run within scope
        // }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}