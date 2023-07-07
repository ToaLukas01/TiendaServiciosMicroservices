using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Libro.Models;

namespace TiendaServicios0.Api.Libro.Contexto
{
    public class ContextoLibreria: DbContext
    {
        // nuestro "DbContextOptions" PARSEA a la clase actual "ContextoLibreria"
        // para crear el constructor del contexto a la base de datos, y poder setear la cadena de coneccion a la base de datos
        public ContextoLibreria() { }
        
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) 
        { 
        }

        // la propiedad "virtual" se refiere a que esto se podra sobreEscribir el turno
        public virtual DbSet<LibreriaMaterial> libreriaMaterial { get; set; }  

    }
}
