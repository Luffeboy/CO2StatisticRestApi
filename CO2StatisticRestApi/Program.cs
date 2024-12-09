using CO2StatisticRestApi;
using Microsoft.EntityFrameworkCore;
using CO2DatabaseLib;
using CO2DatabaseLib.Models;
using Microsoft.Extensions.DependencyInjection;

//var optionsBuilder = new DbContextOptionsBuilder<DbContextCO2>();
//optionsBuilder.UseSqlServer("Data Source=mssql17.unoeuro.com;Initial Catalog=jeppejeppsson_dk_db_test;Persist Security Info=True;User ID=jeppejeppsson_dk;Password=gk3BR45pbxtGwHnard6f;TrustServerCertificate=True");
//// connection string structure
//DbContextCO2 _dbContext = new(optionsBuilder.Options);
//// clean database table: remove all rows
//_dbContext.Users.Add(new User() { Email = "test@a.com", UserPassword = "abc" });
//_dbContext.Users.Add(new User() { Email = "adasdfa@b.com", UserPassword = "123aabbcc" });
//_dbContext.SaveChanges();

//return;

//var startDate = new DateTime(2024, 8, 1);
//Random rand = new Random();
//int prevValue = 400;
//int prevDay = 1;
//MeasurementRepository repository = new MeasurementRepository();
//while (startDate < DateTime.Now)
//{
//    prevValue += rand.Next(100) - 50;
//    if (prevValue < 0) prevValue = 0;
//    if (prevValue > 2000) prevValue = 2000;
//    repository._dbContext.Measurements.Add(new Measurement() { MeasurementTime = startDate, MeasurementValue = prevValue, SensorId = 1 });
//    startDate = startDate.AddMinutes(5);
//    if (prevDay != startDate.Day)
//    {
//        prevDay = startDate.Day;
//        Console.WriteLine(startDate.Day);
//        repository._dbContext.SaveChanges();
//    }
//}
//return;

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
builder.Services.AddTransient<DBConnection>(); // (new DBConnection());
builder.Services.AddTransient<UserRepository>(); // (new UserRepository());
builder.Services.AddTransient<SensorUserRepository>();
var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
