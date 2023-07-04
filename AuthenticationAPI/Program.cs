using AutoMapper;
using LoginAPI.Configuration;
using LoginAPI.Repositories;
using LoginAPI.Repositories.Interfaces;
using LoginAPI.Services.Interfaces;
using LoginAPI.Services;
using RabbitMQService.Services;
using RabbitMQService.Services.Interfaces;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

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

var scope = app.Services.CreateScope();
var consumeMethod = scope.ServiceProvider.GetService<ILoginService>().CreateCostumer;

builder.Services.BuildServiceProvider()
    .GetService<IMessageProcessor>()
    .SubscribeToMessages("direct_exchange", ExchangeType.Direct , "exemplo_fila", "user_create", consumeMethod);


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
