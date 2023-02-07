using BMS.Data.Api.Utilities;
using BMS.Data.Api.Utils;
using BMS.Sql.Library;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

string? corsPolicyName = "BMS.Data.Api.CorsPolicy";

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
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
        );
    });

    // Controllers
    builder.Services.AddControllers();

    // Swagger/OpenAPI (learn more about configuring  at https://aka.ms/aspnetcore/swashbuckle)
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Database
    string bmsDatabaseConnectionString = builder.Configuration.GetConnectionString("BMSDatabaseConnectionString");
    builder.Services.AddDbContext<BMSDbContext>(
        options => options.UseNpgsql(bmsDatabaseConnectionString));

    // Custom Services
    builder.Services.AddScoped<MqttDataService>();
    builder.Services.AddScoped<CommandDataService>();
    builder.Services.AddScoped<BMS.Sql.Library.Services.ChargeControllerService>();
}

static void RunApp(WebApplicationBuilder builder, string corsPolicyName)
{
    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    ;
    new Timers(app.Services.CreateScope().ServiceProvider.GetService<BMSDbContext>(), builder.Configuration, app.Logger);

    app.UseHttpsRedirection();

    app.UseCors(corsPolicyName);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}