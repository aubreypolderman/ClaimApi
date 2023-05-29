// 2023-03-13 toegevoegd
using Microsoft.EntityFrameworkCore;
using ClaimApi.Data;
using ClaimApi.Model;
using Microsoft.Extensions.Hosting;
using ClaimApi.Controllers;
using ClaimApi;
using ClaimApi.Repository;

// 2023-03-13 toegevoegd

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Update the ContractContext registration
builder.Services.AddDbContext<ContractContext>(options =>
    options.UseInMemoryDatabase("ClaimDB"));

// 2023-03-13 Register the DbContext services with scoped lifetime
builder.Services.AddDbContext<ContractContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<ClaimContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<RepairCompanyContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IRepairCompanyRepository, RepairCompanyRepository>();


// Register the DataSeeder as a scoped service
// builder.Services.AddScoped<IHostedService, DataSeeder>();

// 2023-05-26 Build the service provider
var serviceProvider = builder.Services.BuildServiceProvider();

// 2023-05-26 Get an instance of UserContext
// using var context = serviceProvider.GetRequiredService<UserContext>();

// 2023-05-26 Seed the data
//DataSeeder.SeedData(context);
// builder.Services.AddScoped<DataSeeder>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Resolve and invoke DataSeeder
/*
using (var scope = app.Services.CreateScope())
    
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    //dataSeeder.SeedData();
}
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapUserEndpoints();

app.Run();
