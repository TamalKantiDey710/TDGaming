using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TDGaming.Domain.Interfaces;
using TDGaming.Infrastructure.Data;
using TDGaming.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",
                "https://tdgaming-client-app-egathjdcawfqg0hg.canadacentral-01.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Optional � only if you use cookies/auth headers
    });
});

if (builder.Environment.EnvironmentName != "Test")
{
    //builder.Services.AddDbContext<AppDbContext>(options =>
    //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TDGamingInMemory"));
}

builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("AllowFrontend");
app.UseCors("AllowReactDev");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
public partial class Program { }