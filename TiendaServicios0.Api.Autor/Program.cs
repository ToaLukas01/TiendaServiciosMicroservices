using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Autor.Aplication;
using TiendaServicios0.Api.Autor.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<NuevoAutor>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


const string ConectionName = "TiendaServicios";
var connectionString = builder.Configuration.GetConnectionString(ConectionName);

// agregamos el contxto de la base de datos - aqui agregamos una funcion para setear las opciones y conectar a la base de datos
builder.Services.AddDbContext<ContextoAutor>(options => {
    options.UseNpgsql(connectionString);
});

builder.Services.AddMediatR(typeof(NuevoAutor.ManejadorNuevoAutor).Assembly);

builder.Services.AddAutoMapper(typeof(ConsultarAutor.ManejadorConsultaAutor));



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
