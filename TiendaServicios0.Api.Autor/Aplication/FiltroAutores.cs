using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.Autor.Models;
using TiendaServicios0.Api.Autor.Persistencia;

namespace TiendaServicios0.Api.Autor.Aplication
{
    public class FiltroAutores
    {
        // en esta clase realizaremos todas las consultas de filtros de autores
        
        public class DetalleAutor: IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        // dentro de los "<>" indicamos "<loQueRecivo, loQueDevuelvo>"
        public class ManejadorDetalle : IRequestHandler<DetalleAutor, AutorDto>
        {
            // siempre que querramos modifcar/consultar/etc algo referido al contexto de la base de datos
            // tenemos que hacer referencia a la persistencia del contexto para poder trabajar con y sobre el
            private readonly ContextoAutor _contexto;

            private readonly IMapper _mapper;

            public ManejadorDetalle(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<AutorDto> Handle(DetalleAutor request, CancellationToken cancellationToken)
            {
                var detalleAutor = await _contexto.AutorLibro.Where(a => a.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                
                if(detalleAutor == null)
                {
                    throw new Exception("No se encontro el autor buscado");
                }

                // creamos una variable que represente el resultado del mapeo
                // en el metodo "Map" indicamso "<loQueQueremosMapear, ComoQueremosQueSeTransforme>"("ElObjetoEnCuestion")
                var detalleDto = _mapper.Map<AutorLibro, AutorDto>(detalleAutor);
                return detalleDto;
            }

        }
    }
}
