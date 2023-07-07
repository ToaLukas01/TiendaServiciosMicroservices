using TiendaServicios0.Api.Autor.Models;
using MediatR;
using TiendaServicios0.Api.Autor.Persistencia;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace TiendaServicios0.Api.Autor.Aplication
{
    public class ConsultarAutor
    {
        // esta clase nos servira de midelwer entre la base de datos y el controlador
        // para realisar consultas sobre los autores (todos, uno, detalle, id, etc)


        // esta funcionalidad devolvera una lista de autores de la base de datos 
        public class ListaAutor: IRequest<List<AutorDto>>
        {

        }

        // esta clase maneja la logica de la consulta(querys) a la base de datos
        public class ManejadorConsultaAutor : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            // siempre que querramos modifcar/consultar/etc algo referido al contexto de la base de datos
            // tenemos que hacer referencia a la persistencia del contexto para poder trabajar con y sobre el
            private readonly ContextoAutor _contexto;

            private readonly IMapper _mapper;

            public ManejadorConsultaAutor(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var listaAutores = await _contexto.AutorLibro.ToListAsync();
                var listaDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(listaAutores);
                return listaDto;
            }
        }

    }
}
