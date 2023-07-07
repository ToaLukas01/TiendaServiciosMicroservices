using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Autor.Models;

namespace TiendaServicios0.Api.Autor.Persistencia
{
    public class ContextoAutor: DbContext
        
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options): base(options) 
        {
        }

        // ahora vamos a agregar los modelos que queremos "mapear" para combertirlos en entidades en la base de datos
        // (le damos nombre al modelo de base de datos que queremos crear y le decimos a partir de que modelo lo creara)
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}
