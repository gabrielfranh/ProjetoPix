using AutoMapper;
using CostumerAPI.Configuration;
using CostumerAPI.Controllers;
using CostumerAPI.Repositories;
using CostumerAPI.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

//Add Console logs

builder.Services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<ClienteController>>());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

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
