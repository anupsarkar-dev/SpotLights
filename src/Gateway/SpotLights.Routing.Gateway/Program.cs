using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SpotLights.Routing.Gateway.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthentication();

// Add services to the container.
if (builder.Environment.IsProduction())
{
  builder.Configuration.AddJsonFile(
    "ocelot.production.json",
    optional: false,
    reloadOnChange: true
  );
}
else
{
  builder.Configuration.AddJsonFile(
    "ocelot.production.json",
    optional: false,
    reloadOnChange: true
  );
}

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "SpotLights.Routing.Gateway Running...");

await app.UseOcelot();
await app.RunAsync();
