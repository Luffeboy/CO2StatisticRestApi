using CO2StatisticRestApi;
using Microsoft.EntityFrameworkCore;
using CO2DatabaseLib;
using CO2DatabaseLib.Models;
using Microsoft.Extensions.DependencyInjection;

DBConnection dBConnection = new DBConnection();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddTransient<DBConnection>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<SensorUserRepository>();
var app = builder.Build();

app.UseCors("AllowAll");

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
