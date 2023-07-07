using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.CarritoCompras.Models;

namespace TiendaServicios0.Api.CarritoCompras.Contexto
{
    public class ContextoCarrito: DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options) 
        {
        }

        // aqui combertimos las clases modelos en clases entidades para la base de datos
        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoDetalle> CarritoDetalle { get; set; }

    }
}
