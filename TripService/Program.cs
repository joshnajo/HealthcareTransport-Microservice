using Microsoft.EntityFrameworkCore;
using TripService.Data;
using TripService.EventProcessing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// In-memory database
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));

// Dependency Injection
builder.Services.AddScoped<ITripRepo, TripRepo>();
builder.Services.AddControllers();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
// AutoMapper Configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
