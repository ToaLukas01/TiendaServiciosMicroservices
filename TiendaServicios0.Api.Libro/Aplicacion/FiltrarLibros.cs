using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Libro.Contexto;
using TiendaServicios0.Api.Libro.Models;

namespace TiendaServicios0.Api.Libro.Aplicacion
{
    public class FiltrarLibros
    {
        public class DetalleLibro: IRequest<LibroDto>
        {
            // indicamos el parametro que ingresara para realizar la busqueda 
            public Guid? LibroId { get; set; }
        }

        public class ManejadorDetalleLibro : IRequestHandler<DetalleLibro, LibroDto>
        {
            // nececitamso una instancia del contexto de EntityFramework, y una instancia de Mapper
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            // hacemos el constructor para realizar la inyeccion
            public ManejadorDetalleLibro(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroDto> Handle(DetalleLibro request, CancellationToken cancellationToken)
            {
                var detalleLibro = await _contexto.libreriaMaterial.Where(l => l.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                
                if(detalleLibro == null)
                {
                    throw new Exception("No se encontro el libro solicitado");
                }

                var detalleLibroDto = _mapper.Map<LibreriaMaterial, LibroDto>(detalleLibro);
                return detalleLibroDto;

            }
            
        }
        
    }
}
