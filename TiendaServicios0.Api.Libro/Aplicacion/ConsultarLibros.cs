using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Libro.Contexto;
using TiendaServicios0.Api.Libro.Models;

namespace TiendaServicios0.Api.Libro.Aplicacion
{
    public class ConsultarLibros
    {
        //                                      "<QueDevolvemos>"
        public class ConsultarLibreria: IRequest<List<LibroDto>>
        {

        }

        //                                                      "<Recive, Devuelve>"           
        public class ManejadorConsultarLibreria : IRequestHandler<ConsultarLibreria, List<LibroDto>>
        {
            // nececitamso una instancia del contexto de EntityFramework, y una instancia de Mapper
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            // hacemos el constructor para realizar la inyeccion
            public ManejadorConsultarLibreria(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroDto>> Handle(ConsultarLibreria request, CancellationToken cancellationToken)
            {
                var listaLibros = await _contexto.libreriaMaterial.ToListAsync();

                // metodo Mpa: "<TipoDeDatoOrigen, AqueLoQueremosMapar/AqueQueremosQueSeCombierta>" "(ElObjeteoQueSeVaTransformar)"
                var listaLibrosDto = _mapper.Map<List<LibreriaMaterial>, List<LibroDto>>(listaLibros);

                return listaLibrosDto;
            }
        }
    }
}
