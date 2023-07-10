using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using TiendaServicios0.Api.Gateway.ImplementRemote;
using TiendaServicios0.Api.Gateway.InterfaceRemote;
using TiendaServicios0.Api.Gateway.MessageHandler;
using TiendaServicios0.Api.Gateway.ModelsRemote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// IMPLEMENTAMOS LOS SERVICIOS PARA Autor
// recordar: "<"Desde_Donde_Lo_Llamo", "A_Donde_Lo_Implemento">"
builder.Services.AddSingleton<IAutorRemote, AutorImplement>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IMPLEMENTAMOS LAS FUNCIONALIDADES Http Y TAMBIEN LO CONFIGURAMOS PARA TRABAJAR CON LOS SERVICIOS QU HEMOS CREADO
builder.Services.AddHttpClient("AutorService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autor"]);
});

// INICIALIZAMOS "Ocelot" PARA IMPLEMENTAR LA LIBRERIA DE APIs GATEWAY
builder.Services.AddOcelot().AddDelegatingHandler<LibroHandler>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// esta linea corresponde a la implementacion de "Ocelot" para utlizar APIs Gateway
await app.UseOcelot();

app.Run();
