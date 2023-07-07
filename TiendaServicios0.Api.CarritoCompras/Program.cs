using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.CarritoCompras.Contexto;
using TiendaServicios0.Api.CarritoCompras.Mediador;
using TiendaServicios0.Api.CarritoCompras.RemoteInterface;
using TiendaServicios0.Api.CarritoCompras.RemoteServices;

var builder = WebApplication.CreateBuilder(args);

// INSTANCIAMOS LA INTERFAZ DE SERVICIO QUE HEMOS CREADO PARA CONSULTAR LOS ENDPOINTS DE LA MICROSERVICE DE LIBROS
// recordar: "<"Desde_Donde_Lo_Llamo", "A_Donde_Lo_Implemento">"
builder.Services.AddScoped<ILibrosServices, LibrosServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AGREGAMOS EL CONTEXTO
const string ConectionName = "CarritoComprasMicroservices";
var connectionString = builder.Configuration.GetConnectionString(ConectionName);

builder.Services.AddDbContext<ContextoCarrito>(options =>
{
    //options.UseMySQL(connectionString); // -> intente usar MySQL pero me daba erro asi que cambie de base de datos
    options.UseSqlServer(connectionString);
});

// INSTANCIAMOS EL MEDIADOR DE "MediatR" (en "typeof" indicamos en donde se lo llama para inicializarlo)
builder.Services.AddMediatR(typeof(NuevoCarrito.ManejadorCrearCarrito).Assembly);

// INSTANCIAMOS EL "HttpCLient"
// la comunicacion sincrona entre 2/mas microservices se da a travez de los protocolos HTTP
// para ello debemos intanciar el uso de esos protocolos
// dentro indicamos el nombre de la base EndPoint que queremos consumir
// y luego indicamos la configuracion de esta misma
builder.Services.AddHttpClient("Libros", config =>
{
    // recordar que "builder.Configuration" nos permite leer los valores que estan en el appsettings
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});


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
