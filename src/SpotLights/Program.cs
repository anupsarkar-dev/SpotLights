using SpotLights;
using SpotLights.Blogs;
using SpotLights.Caches;
using SpotLights.Data;
using SpotLights.Shared.Extensions;
using SpotLights.Options;
using SpotLights.Posts;
using SpotLights.Shared.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using SpotLights.Data.Identity;
using SpotLights.Data.Storages;
using SpotLights.Data.Newsletters;
using SpotLights.Shared;


var builderMigrations = WebApplication.CreateBuilder(args);
builderMigrations.Host.UseSerilog((context, builder) =>
  builder.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());
builderMigrations.Services.AddDbContext(builderMigrations.Environment, builderMigrations.Configuration);
var appMigrations = builderMigrations.Build();
await appMigrations.RunDbContextMigrateAsync();
await appMigrations.DisposeAsync();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, builder) =>
  builder.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

builder.Services.AddHttpClient();
builder.Services.AddLocalization();

builder.Services.AddDbContext(builder.Environment, builder.Configuration);
builder.Services.AddCache(builder.Environment, builder.Configuration);

builder.Services.AddIdentity();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
  .AddIdentityCookies();
builder.Services.AddAuthorization();

builder.Services.AddStorageStaticFiles(builder.Configuration);

builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

builder.Services.AddScoped<MarkdigProvider>();
builder.Services.AddScoped<ReverseProvider>();
builder.Services.AddScoped<ImportRssProvider>();
builder.Services.AddScoped<UserProvider>();
builder.Services.AddScoped<PostProvider>();
builder.Services.AddScoped<CategoryProvider>();
builder.Services.AddScoped<NewsletterProvider>();
builder.Services.AddScoped<SubscriberProvider>();

builder.Services.AddScoped<OptionProvider>();
builder.Services.AddScoped<AnalyticsProvider>();
builder.Services.AddScoped<EmailManager>();
builder.Services.AddScoped<ImportManager>();
builder.Services.AddScoped<PostManager>();
builder.Services.AddScoped<BlogManager>();
builder.Services.AddScoped<MainMamager>();

builder.Services.AddCors(option =>
{
  option.AddPolicy(SpotLightsConstant.PolicyCorsName, builder =>
  {
    builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
  options.KnownNetworks.Clear();
  options.KnownProxies.Clear();
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddResponseCaching();
builder.Services.AddOutputCache(options =>
{
  options.AddPolicy(SpotLightsConstant.OutputCacheExpire1, builder =>
    builder.Expire(TimeSpan.FromMinutes(15)));
});

builder.Services.AddControllersWithViews()
  .AddDataAnnotationsLocalization(options =>
    options.DataAnnotationLocalizerProvider =
      (type, factory) => factory.Create(typeof(Resource)));
builder.Services.AddRazorPages().AddViewLocalization();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/404");
}

app.UseForwardedHeaders();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStorageStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseRequestLocalization(new RequestLocalizationOptions()
  .AddSupportedCultures(SpotLightsConstant.SupportedCultures)
  .AddSupportedUICultures(SpotLightsConstant.SupportedCultures));
app.UseCors(SpotLightsConstant.PolicyCorsName);
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseOutputCache();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapFallbackToFile("admin/{*path:nonfile}", "index.html");

await app.RunAsync();
