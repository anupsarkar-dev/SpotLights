using System;
using Microsoft.EntityFrameworkCore;
using SpotLights.Course.Core.Interfaces;
using SpotLights.Course.Core.Services;
using SpotLights.Course.Infrastructure.Data;
using SpotLights.Course.Infrastructure.Interfaces;
using SpotLights.Course.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CourseDbContext>(option =>
{
  option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

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
    var dbContext = scope.ServiceProvider.GetRequiredService<CourseDbContext>();

    if (dbContext.Database.GetPendingMigrations().Count() > 0)
    {
      dbContext.Database.Migrate();
    }
  }
}
