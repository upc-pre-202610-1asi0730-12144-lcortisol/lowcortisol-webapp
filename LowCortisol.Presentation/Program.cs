using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.DeviceManagement.Services;
using LowCortisol.Application.Contexts.IdentityAccess.Services;
using LowCortisol.Application.Contexts.IncidentManagement.Services;
using LowCortisol.Application.Contexts.Monitoring.Contracts;
using LowCortisol.Application.Contexts.Monitoring.Services;
using LowCortisol.Application.Contexts.NotificationManagement.Contracts;
using LowCortisol.Application.Contexts.NotificationManagement.Services;
using LowCortisol.Application.Contexts.Reporting.Services;
using LowCortisol.Application.Security;
using LowCortisol.Infrastructure.Monitoring;
using LowCortisol.Infrastructure.Notifications;
using LowCortisol.Infrastructure.Persistence;
using LowCortisol.Infrastructure.Persistence.Context;
using LowCortisol.Infrastructure.Persistence.Repositories;
using LowCortisol.Infrastructure.Security;
using LowCortisol.Domain.Contexts.DeviceManagement.Repositories;
using LowCortisol.Domain.Contexts.IdentityAccess.Repositories;
using LowCortisol.Domain.Contexts.IncidentManagement.Repositories;
using LowCortisol.Domain.Contexts.Monitoring.Repositories;
using LowCortisol.Domain.Contexts.NotificationManagement.Repositories;
using LowCortisol.Domain.Contexts.Reporting.Repositories;
using LowCortisol.Presentation.Components;
using LowCortisol.Presentation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IMonitoringRepository, MonitoringRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IConsumptionReportRepository, ConsumptionReportRepository>();

builder.Services.AddScoped<IUnitOfWork, AppUnitOfWork>();

builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<IThresholdEvaluator, ThresholdEvaluator>();
builder.Services.AddScoped<IMonitoringAlertFactory, MonitoringAlertFactory>();
builder.Services.AddScoped<INotificationSender, ConsoleNotificationSender>();

builder.Services.AddScoped<IIdentityAccessService, IdentityAccessService>();
builder.Services.AddScoped<IDeviceManagementService, DeviceManagementService>();
builder.Services.AddScoped<IMonitoringService, MonitoringService>();
builder.Services.AddScoped<IIncidentManagementService, IncidentManagementService>();
builder.Services.AddScoped<INotificationManagementService, NotificationManagementService>();
builder.Services.AddScoped<IReportingService, ReportingService>();

builder.Services.AddScoped<CurrentUserSession>();
builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();

builder.Services.AddScoped<UiSessionService>();
builder.Services.AddScoped<I18nService>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<BrowserSessionService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();