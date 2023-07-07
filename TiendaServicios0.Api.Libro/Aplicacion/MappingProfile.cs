using AutoMapper;
using TiendaServicios0.Api.Libro.Models;

namespace TiendaServicios0.Api.Libro.Aplicacion
{
    // esta clase se encargara de mapear los modelos a objetos DTO
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // en este metodo dentro de los "<Origen, Destino>"
            CreateMap<LibreriaMaterial, LibroDto>();
        }
    }
}
