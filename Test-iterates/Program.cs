using Test_iterates.Entities;
using Test_iterates.Services.BeerServices;
using Test_iterates.Services.ClientServices;
using Test_iterates.Services.SaleServices;
using Test_iterates.Services.TestServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITestServices, TestServices>();
builder.Services.AddScoped<IBeerServices, BeerServices>();
builder.Services.AddScoped<ISaleServices, SaleServices>();
builder.Services.AddScoped<IClientServices, ClientServices>();

builder.Services.AddAutoMapper(typeof(Beer));
builder.Services.AddAutoMapper(typeof(WholesalerBeerStock));
builder.Services.AddAutoMapper(typeof(Client));
builder.Services.AddAutoMapper(typeof(Request));

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
