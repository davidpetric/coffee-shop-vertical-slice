using Api.EndpointsExtensions;

using Application;
using Application.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .AddUserSecrets(typeof(Program).Assembly, optional: true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Description = "Coffee Shop API v1",
            Version = "v1",
            Title = "Coffee Shop API v1",
        }
    );
});

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddProblemDetails();

builder.Services.AddEndpoints(typeof(ConfigureApplicationServices).Assembly);

WebApplication app = builder.Build();

app.MapGroup("api");

app.RegisterEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(opt =>
    {
        const string title = "Coffee Shop API v1";
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", title);

        opt.DocumentTitle = title;
        opt.RoutePrefix = "api-doc";
    });
}

if (app.Environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();

    CoffeeShopDbContext dbContext = scope.ServiceProvider.GetRequiredService<CoffeeShopDbContext>();

    await dbContext.Database.OpenConnectionAsync();
    await dbContext.Database.EnsureCreatedAsync();

    DbSeed.Seed(context: dbContext);

    app.Map("/", () => Results.Redirect("/api-doc"));
}

await app.RunAsync();

public partial class Program
{
    protected Program() { }
}
