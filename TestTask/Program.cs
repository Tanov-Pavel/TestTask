using Client.Interfaces;
using Client.Service;
using Domain;
using Repository;
using Repository.Repositories;
using Repository.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();      // подключема сервисы SignalR

builder.Services.AddScoped<IMetricRepository, MetricRepository>();
builder.Services.AddScoped<IDiskSpaceRepository, DiskSpaceRepository>();

builder.Services.AddScoped<IDiskSpaceService, DiskSpaceService>();

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
app.MapHub<TestTask.SignalRChat>("/chat");   // ChatHub будет обрабатывать запросы по пути /chat
var str = "Server=localhost;Port=5432;Database=systemmetrixdb;UserId=postgres;Password=123456";
var strr = "Driver={PostgreSQL};Server=localhost;Port=5432;Database=systemmetrixdb;Uid=postgres;Pwd=123456;";

string url = "http://127.0.0.1:8088";
app.Run(url);
Console.WriteLine(app.ToString());