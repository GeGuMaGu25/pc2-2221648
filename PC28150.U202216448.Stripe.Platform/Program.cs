using PC28150.U202216448.Stripe.Platform.Billing.Application.CommandServices;
using PC28150.U202216448.Stripe.Platform.Billing.Application.Internal.CommandServices;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Repositories;
using PC28150.U202216448.Stripe.Platform.Billing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using PC28150.U202216448.Stripe.Platform.Resources.Errors;
using PC28150.U202216448.Stripe.Platform.Resources.Shared;
using PC28150.U202216448.Stripe.Platform.Shared.Domain.Repositories;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scalar.AspNetCore;
using ProblemDetailsFactory = PC28150.U202216448.Stripe.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails and ExceptionHandler services
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add Database Connection

// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .AddInterceptors(serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Resources are resolved by the full name of their marker classes (e.g. Resources.Errors.ErrorMessages)
builder.Services.AddLocalization();

// Explicitly register IStringLocalizer for ErrorMessages and Commons
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

// OpenAPI / Scalar documentation
builder.Services.AddOpenApi();

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();

// Billing Bounded Context
builder.Services.AddScoped<IPaymentMandateRepository, PaymentMandateRepository>();
builder.Services.AddScoped<IPaymentMandateCommandService, PaymentMandateCommandService>();

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(ExceptionHandlingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(TransactionCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    [typeof(Program)]);

var app = builder.Build();

// Apply pending migrations on startup (safe to call even when schema is up to date)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var supportedCultures = new[] { "en", "en-US", "es", "es-PE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
