using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.DeviceManagement.Services;
using LowCortisol.Application.Contexts.IdentityAccess.Services;
using LowCortisol.Application.Contexts.IncidentManagement.Services;
using LowCortisol.Application.Contexts.Monitoring.Services;
using LowCortisol.Application.Contexts.NotificationManagement.Services;
using LowCortisol.Application.Contexts.Reporting.Services;
using LowCortisol.Application.Security;
using LowCortisol.Infrastructure.Monitoring;
using LowCortisol.Infrastructure.Notifications;
using LowCortisol.Infrastructure.Persistence;
using LowCortisol.Infrastructure.Persistence.Context;
using LowCortisol.Infrastructure.Persistence.Repositories;
using LowCortisol.Infrastructure.Security;
using LowCortisol.Presentation.Components;
using LowCortisol.Presentation.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Forwarded headers for Render / reverse proxy
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedHost;

    // Accept forwarded headers from the proxy/load balancer
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<LowCortisol.Domain.Contexts.IdentityAccess.Repositories.IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<LowCortisol.Domain.Contexts.DeviceManagement.Repositories.IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<LowCortisol.Domain.Contexts.Monitoring.Repositories.IMonitoringRepository, MonitoringRepository>();
builder.Services.AddScoped<LowCortisol.Domain.Contexts.IncidentManagement.Repositories.IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<LowCortisol.Domain.Contexts.NotificationManagement.Repositories.INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<LowCortisol.Domain.Contexts.Reporting.Repositories.IConsumptionReportRepository, ConsumptionReportRepository>();

// Common
builder.Services.AddScoped<IUnitOfWork, AppUnitOfWork>();

// Security / Infra services
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<CurrentUserSession>();
builder.Services.AddScoped<LowCortisol.Application.Contexts.Monitoring.Contracts.IThresholdEvaluator, ThresholdEvaluator>();
builder.Services.AddScoped<LowCortisol.Application.Contexts.NotificationManagement.Contracts.INotificationSender, ConsoleNotificationSender>();
builder.Services.AddScoped<LowCortisol.Application.Contexts.Monitoring.Contracts.IMonitoringAlertFactory, MonitoringAlertFactory>();

// Application services
builder.Services.AddScoped<IIdentityAccessService, IdentityAccessService>();
builder.Services.AddScoped<IDeviceManagementService, DeviceManagementService>();
builder.Services.AddScoped<IMonitoringService, MonitoringService>();
builder.Services.AddScoped<IIncidentManagementService, IncidentManagementService>();
builder.Services.AddScoped<INotificationManagementService, NotificationManagementService>();
builder.Services.AddScoped<IReportingService, ReportingService>();

// Presentation services
builder.Services.AddScoped<BrowserSessionService>();
builder.Services.AddScoped<UiSessionService>();
builder.Services.AddScoped<I18nService>();

var app = builder.Build();

// Respect proxy headers BEFORE redirects/routing
app.UseForwardedHeaders();

// Database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// On Render this is usually fine once forwarded headers are enabled
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();