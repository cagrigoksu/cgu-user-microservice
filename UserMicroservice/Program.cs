using System;
using Microsoft.EntityFrameworkCore;
using UserMicroservice;
using UserMicroservice.Services.Interfaces;
using UserMicroservice.Services;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("AzureSQLConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

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
