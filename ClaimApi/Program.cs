// 2023-03-13 toegevoegd
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;
using Microsoft.Extensions.Hosting;
using ClaimApi.Controllers;
using ClaimApi;
using ClaimApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ClaimApi.Auth0;
using ClaimApi.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

// 2023-03-13 toegevoegd
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
            .WithOrigins("http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

// 2023-05-29 add authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services
  .AddAuthorization(options =>
  {
      options.AddPolicy(
        "read:messages",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:messages", domain)
        )
      );
  });

builder.Services.AddControllers();

// 2023-03-13 Register the DbContext services with scoped lifetime
builder.Services.AddDbContext<ContractContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<ClaimFormContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<RepairCompanyContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IRepairCompanyRepository, RepairCompanyRepository>();
builder.Services.AddScoped<IClaimFormRepository, ClaimFormRepository>();

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

// 2023-05-26 Build the service provider
var serviceProvider = builder.Services.BuildServiceProvider();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var userContext = serviceScope.ServiceProvider.GetRequiredService<UserContext>();
    var repairCompanyContext = serviceScope.ServiceProvider.GetRequiredService<RepairCompanyContext>();
    DataSeeder.SeedData(userContext, repairCompanyContext);
    Debug.WriteLine(DateTime.Now + "[--------] [program.cs] na uitvoer van dataSeeder");
}

app.Run();
