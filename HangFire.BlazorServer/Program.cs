using HangFire.BlazorServer.Data;
using HangFire.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using NetCore.AutoRegisterDi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<HangFireBlazorServerDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

ConfigureAutoMapper(builder);

InjectPatternFromAssemblies(builder, "Repository");
InjectPatternFromAssemblies(builder, "Service");

var app = builder.Build();

await EnsureDbIsMigrated(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

async Task EnsureDbIsMigrated(IServiceProvider services)
{
    using var scope = services.CreateScope();
    using var ctx = scope.ServiceProvider.GetService<HangFireBlazorServerDbContext>();
    if (ctx != null)
    {
        await ctx.Database.MigrateAsync();
    }
}

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
