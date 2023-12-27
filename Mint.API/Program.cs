using DataAccessLayer.Coingecko;
using Interfaces.BusinessLogicLayer;
using Interfaces.DataAccessLayer;
using Mint.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services
builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<IApiServiceCoingecko, ApiServiceCoingecko>();
builder.Services.AddHttpClient<IApiServiceCoingecko, ApiServiceCoingecko>();

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
