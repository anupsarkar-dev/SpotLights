using Microsoft.EntityFrameworkCore;
using SpotLights.Certificaion.Infrastructure.Data;
using SpotLights.Certification.Core.Interfaces;
using SpotLights.Certification.Core.Services;
using SpotLights.Certification.Infrastructure.Interfaces;
using SpotLights.Certification.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CertificateDbContext>(option =>
{
  option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
  );
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICertificationService, CertificationService>();
builder.Services.AddScoped<ICertificationRepository, CertificatationRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();

//}

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

await app.RunAsync();

void ApplyMigration()
{
  using (var scope = app.Services.CreateScope())
  {
    var dbContext = scope.ServiceProvider.GetRequiredService<CertificateDbContext>();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
      dbContext.Database.Migrate();
    }
  }
}
