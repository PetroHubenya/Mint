using DataAccessLayer;
using Helpers;
using Interfaces.BusinessLogicLayer;
using Interfaces.DataAccessLayer;
using Mint.BLL;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<IApiService, ApiServiceCoingecko>();
// builder.Services.AddHttpClient<IApiService, ApiServiceCoingecko>();
builder.Services.AddHttpClient<IApiService, ApiServiceCoincap>();
builder.Services.AddSingleton<CoincapDataToCoinMapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
