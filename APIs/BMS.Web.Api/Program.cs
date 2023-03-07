using BMS.Sql.Library;
using BMS.Sql.Library.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Text.Json.Serialization;
using BMS.Web.Api.Authentication;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

string? corsPolicyName = "BMS.Web.Api.CorsPolicy";

AddServices(builder, corsPolicyName);

RunApp(builder, corsPolicyName);

static void AddServices(WebApplicationBuilder builder, string corsPolicyName)
{
    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: corsPolicyName,
            builder =>
            {
                builder
                    .WithOrigins(
                        "http://localhost:4200",
                        "https://azapp-bms-web-frontend-dev.azurewebsites.net",
                        "https://azapp-bms-web-frontend-prod.azurewebsites.net"
                    ).AllowAnyMethod()
                    .AllowAnyHeader();
            }
        );
    });

    // Controllers
    builder.Services
        .AddControllers()
        .AddJsonOptions(opts =>
        {
            var enumConverter = new JsonStringEnumConverter();
            opts.JsonSerializerOptions.Converters.Add(enumConverter);
        });

    // Swagger/OpenAPI (learn more about configuring  at https://aka.ms/aspnetcore/swashbuckle)
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Database
    string bmsDatabaseConnectionString = builder.Configuration.GetConnectionString("BMSDatabaseConnectionString");

    builder.Services.AddDbContext<BMSDbContext>(
        options => options.UseNpgsql(bmsDatabaseConnectionString));

    // Identity
    builder.Services.AddEntityFrameworkNpgsql()
        .AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<BMSDbContext>()
        .AddRoles<IdentityRole>()
        .AddDefaultTokenProviders();

    builder.Services.AddScoped<IdentityUserService>();
    builder.Services
        .AddScoped<BMS.Web.Api.Authentication.AuthenticationService>()
        .AddScoped<ApplicationUserService>()
        .AddScoped<JwtService>();

    // Authentication
    AddJwtAuthentication(builder.Services, builder.Configuration);
}

static void AddJwtAuthentication(IServiceCollection services, ConfigurationManager configuration)
{
    string secret = configuration.GetSection("JwtConfig").GetSection("Secret").Value;
    byte[] key = Encoding.ASCII.GetBytes(secret);
    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

static async void RunApp(WebApplicationBuilder builder, string corsPolicyName)
{
    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(corsPolicyName);

    app.UseAuthorization();

    app.UseAuthentication();

    app.MapControllers();

    app.Run();
}