using Microsoft.EntityFrameworkCore;
using WebApi_2025_jueves.DAL;
using WebApi_2025_jueves.DAL.Entities;
using WebApi_2025_jueves.Domain.Interfaces;
using WebApi_2025_jueves.Domain.Services;
using DataBaseContext = WebApi_2025_jueves.DAL.Entities.DataBaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Esta es la linea de codigo que necesito para configurar la conexion a la BD
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString
    ("Defaulconnection")));

//Contenedor de Dependencias
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddTransient<SeederDB>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeederData();
void SeederData()
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopeFactory?.CreateScope())
    {
        SeederDB? service = scope?.ServiceProvider.GetService<SeederDB>();
        service.SeedAsync().Wait();
    }
}

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
