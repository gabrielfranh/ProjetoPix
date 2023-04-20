using AutoMapper;
using KeyAPI.Configuration;
using KeyAPI.Repositories;
using KeyAPI.Repositories.Interfaces;
using KeyAPI.Services;
using KeyAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IKeyService, KeyService>();
builder.Services.AddScoped<IKeyRepository, KeyRepository>();

builder.Services.AddHttpClient<IKeyService, KeyService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CostumerApi"])
    );

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
