using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios0.Api.Libro.Aplicacion;
using TiendaServicios0.Api.Libro.Models;

namespace TiendaServicios0.Api.Libros.Test
{
    // esta clase Emulara el funcionamiento de "IMapper"
    public class MappingTest: Profile
    {
        public MappingTest() 
        {
            CreateMap<LibreriaMaterial, LibroDto>();
        }
    }
}
