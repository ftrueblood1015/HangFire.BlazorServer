using Hangfire;
using Hangfire.LiteDB;
using HangFire.Infrastructure;
using HangFire.JobServer;
using HangFire.Services.Services;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<HangFireBlazorServerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
GlobalConfiguration.Configuration.UseLiteDbStorage();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings());

builder.Services.AddHangfireServer();

ConfigureAutoMapper(builder);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var config = new ConfigurationBuilder().AddJsonFile($"appsettings.{env}.json").AddEnvironmentVariables().Build();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(config.GetValue<string>("AppBaseUrl")!) });
builder.Services.AddScoped(sp => new ScryfallApiServerClient(new HttpClient { BaseAddress = new Uri(config.GetValue<string>("ScryfallApiServerBaseUrl")!) }));

InjectPatternFromAssemblies(builder, "Repository");
InjectPatternFromAssemblies(builder, "Service");

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHangfireDashboard();

JobSetUp.LoadJobs();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

app.MapRazorPages();

app.Run();

void InjectPatternFromAssemblies(WebApplicationBuilder builder, string pattern, params Assembly[] assembly)
{
    builder.Services.RegisterAssemblyPublicNonGenericClasses(GetAssemblies())
            .Where(c => c.Name.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase))
            .AsPublicImplementedInterfaces();
}

static Assembly[] GetAssemblies()
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("HangFire")).ToList();

    assemblies.Add(Assembly.Load("HangFire.Services"));
    assemblies.Add(Assembly.Load("HangFire.Domain"));
    assemblies.Add(Assembly.Load("HangFire.Infrastructure"));

    return assemblies.ToArray();
}

static void ConfigureAutoMapper(WebApplicationBuilder builder)
{
    builder.Services.AddAutoMapper(GetAssemblies());
}
