using favorites.Models;
using favorites.Repositories;
using favorites.Repositories.Interfaces;
using favorites.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FavoriteContext>(options => options.UseSqlServer("Data Source=DESKTOP-4KKPTIM; Initial Catalog=favorites; Integrated Security=SSPI; TrustServerCertificate=true"));

// Services
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.Run();
