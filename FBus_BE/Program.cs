using FBus_BE.Models;
using FBus_BE.Services;
using FBus_BE.Services.Implements;
using FBus_BE.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FbusMainContext>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IRouteStationService, RouteStationService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
// });
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
