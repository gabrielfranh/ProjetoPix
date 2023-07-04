using AutoMapper;
using CostumerAPI.Configuration;
using CostumerAPI.Repositories;
using CostumerAPI.Repositories.Interface;
using CostumerAPI.Services;
using CostumerAPI.Services.Interfaces;
using RabbitMQService.Services.Interfaces;
using RabbitMQService.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ICostumerRepository, CostumerRepository>();
builder.Services.AddScoped<ICostumerService, CostumerService>();
builder.Services.AddSingleton<IMessageProcessor>(sp =>
{
    string hostname = "localhost";
    int port = 5672;
    string username = "gabrielfranh";
    string password = "3J!hq6PRicach!e";
    string virtualHost = "/";

    return new MessageProcessor(hostname, port, username, password, virtualHost);
});

// Mapper
IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

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
