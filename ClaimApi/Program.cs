// 2023-03-13 toegevoegd
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;
// 2023-03-13 toegevoegd

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 2023-03-13 toegevoegd
builder.Services.AddDbContext<ContractContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<ClaimContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<RepairCompanyContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("ClaimDB"));
// 2023-03-13 toegevoegd

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
