using CustomersList.Api.Settings;
using CustomersList.Application;
using CustomersList.Infrastructure;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();


builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.Configure<ApplicationSettings>(
    options =>
    {
        configuration
        .GetSection("ApplicationSettings")
        .Bind(options);
    });

builder.Services.AddFastEndpoints(o => o.IncludeAbstractValidators = true)
    .AddAuthenticationJwtBearer(options =>
    {
        var key = configuration.GetRequiredSection("ApplicationSettings:Authentication").GetValue<string>("SecretKey");
        options.SigningKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(key));
        options.SigningStyle = TokenSigningStyle.Symmetric;
    })
    .AddAuthorization()
    .AddFastEndpoints()
.SwaggerDocument();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();
