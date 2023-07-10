using TiendaServicios0.Api.Libro.Contexto;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Libro.Aplicacion;
using MediatR;
using FluentValidation.AspNetCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// INICIALIZAMOS EL VALIDADOR DE "FluentValidator" aqui en esta parte donde se agregan los controladores
// y en su configuracion agregamos una funcion donde le indicamso "<EnDondeEstaLoQueQueremosValidar>"
builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<NuevoLibro>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AGREGAMOS EL CONTEXTO
const string ConectionName = "TiendaLibros"; 
var connectionString = builder.Configuration.GetConnectionString(ConectionName);

builder.Services.AddDbContext<ContextoLibreria>(options =>
{
    options.UseSqlServer(connectionString);
});

// INSTANCIAMOS EL MEDIADOR DE "MediatR" (en "typeof" indicamos en donde se lo llama para inicializarlo)
builder.Services.AddMediatR(typeof(NuevoLibro.ManejadorNuevoLibro).Assembly);

// INSTANCIAMOS EL MAPEADR PARA LOS OBJEOS DTO de "AutoMapper" (en "typeof" indicamos en donde se lo llama para inicializarlo)
builder.Services.AddAutoMapper(typeof(ConsultarLibros.ConsultarLibreria));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
