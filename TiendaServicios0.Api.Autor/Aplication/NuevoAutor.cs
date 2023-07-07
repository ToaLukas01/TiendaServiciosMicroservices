using FluentValidation;
using MediatR;
using TiendaServicios0.Api.Autor.Models;
using TiendaServicios0.Api.Autor.Persistencia;

namespace TiendaServicios0.Api.Autor.Aplication
{
    public class NuevoAutor
    {
        // aqui crearemos 2 clases,
        // 1: en una de ellas recivira los parametros que se envian al controlador
        // 2: y en la segunda clase, implementaremos la logica de la insercion en la base de datos

        public class EjecutarAutor : IRequest
        {
            // aqui definire la estructura de los parametros que nos envia el controlador
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class ValidarAutor: AbstractValidator<EjecutarAutor>
        {
            public ValidarAutor()
            {
                RuleFor(a => a.Nombre).NotEmpty();
                RuleFor(a => a.Apellido).NotEmpty();
            }
        }

        public class ManejadorNuevoAutor : IRequestHandler<EjecutarAutor>
        {
            // aqui crearemos la logica de la transaccion para insertar un nuevo autor

            public readonly ContextoAutor _contexto; // creamso una referencia al contexto

            public ManejadorNuevoAutor(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(EjecutarAutor request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Guid.NewGuid().ToString(),
                };

                _contexto.AutorLibro.Add(autorLibro);
                var valor = await _contexto.SaveChangesAsync();

                if(valor > 0)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new Exception("No se pudo insertar el autor de libro");
                }
            }
        }

    }
}
