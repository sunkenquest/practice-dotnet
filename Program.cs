using System;
using practice_dotnet.Data;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using practice_dotnet.Repository.Interface;
using practice_dotnet.Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
	new MySqlServerVersion(new Version(8, 0, 39))));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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
