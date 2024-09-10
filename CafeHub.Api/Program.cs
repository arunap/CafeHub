using CafeHub.Api.Services;
using CafeHub.Application;
using CafeHub.Application.Common.Contracts;
using CafeHub.Infrastructure;
using CafeHub.Infrastructure.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register project dependencies
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DefaultFrontEndClient",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var sp = app.Services.CreateScope();
    var cafeSeeds = sp.ServiceProvider.GetRequiredService<CafeDataInitializer>();
    await cafeSeeds.SeedAsync();

    var employeeSeeds = sp.ServiceProvider.GetRequiredService<EmployeeDataInitializer>();
    await employeeSeeds.SeedAsync();

}

app.UseCors("DefaultFrontEndClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
