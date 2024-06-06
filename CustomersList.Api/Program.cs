using CustomersList.Api.Middleware;
using CustomersList.Api.Settings;
using CustomersList.Application;
using CustomersList.Application.Repositories;
using CustomersList.Application.Services.Authentication;
using CustomersList.Application.Services.Customers;
using CustomersList.Application.Services.Users;
using CustomersList.Infrastructure.Repositories;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using FluentValidation;
using Serilog;


var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.Configure<ApplicationSettings>(
    options =>
    {
        configuration
        .GetSection("ApplicationSettings")
        .Bind(options);
    });
// Add services to the container.
builder.Services.AddSingleton<ICustomersRepository, CustomersRepository>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

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

builder.Services.AddTransient(typeof(IPreProcessor<>),typeof(ValidationsPreprocessor<>));
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
