using System;
using Microsoft.EntityFrameworkCore;
using SpotLights.Course.Core.Services;
using SpotLights.Quiz.Core.Interfaces;
using SpotLights.Quiz.Infrastructure.Data;
using SpotLights.Quiz.Infrastructure.Interfaces;
using SpotLights.Quiz.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuizDbContext>(option =>
{
  option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();

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

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

await app.RunAsync();

void ApplyMigration()
{
  using (var scope = app.Services.CreateScope())
  {
    var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
      dbContext.Database.Migrate();
    }
  }
}
