using AutoMapper;
using CarSalesWebAPI.Data;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Services.Interfaces.Services;
using CarSalesWebAPI.Services.Profiles;
using CarSalesWebAPI.Services.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IAssessmentRecordService, AssessmentRecordService>();

builder.Services.AddAutoMapper(typeof(UserProfile), typeof(AssessmentRecordProfile), typeof(CarProfile));

var connection = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
