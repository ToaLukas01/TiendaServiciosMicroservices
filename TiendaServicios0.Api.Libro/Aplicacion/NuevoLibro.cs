using FluentValidation;
using MediatR;
using TiendaServicios0.Api.Libro.Contexto;
using TiendaServicios0.Api.Libro.Models;

namespace TiendaServicios0.Api.Libro.Aplicacion
{
    public class NuevoLibro
    {
        // creamos las clases que serviran de mediador entre las peticiones que nos manden los controladores
        // sobre los datos que nececitemso consultar en la base de datos
        public class CrearLibro : IRequest
        {
            // esta clase representa la estructura de lo que se va a crear
            // indicamos los parametros/propiedades que ingresaran para realizar la insercion del nuevo objeto a la base de datos
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }
        
        // cremos la clase de validacion de datos y le mandamos dentro de "<LoQueQueremosValidar>"
        public class ValidarLibro: AbstractValidator<CrearLibro>
        {
           // creamos el constructor y dentro de el agregamos la logica de la validacion
           public ValidarLibro()
            {
                RuleFor(l => l.Titulo).NotEmpty();
                RuleFor(l => l.FechaPublicacion).NotEmpty();
                RuleFor(l => l.AutorLibro).NotEmpty();
            }
        }

        // creamos la clase de la logica de la transaccion para insertar un nuevo libro en la base de datos
        public class ManejadorNuevoLibro : IRequestHandler<CrearLibro>
        {
            // instancioamos a la persistencia del contexto de la base de datos para poder trabajr con ella
            private readonly ContextoLibreria _contexto;

            public ManejadorNuevoLibro(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(CrearLibro request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro,
                };
                _contexto.libreriaMaterial.Add(libro);
                var value = await _contexto.SaveChangesAsync(); // este metodo devuelve un numero equivalente a la cantidad de registros que se han insertado en la base de datos
                
                if(value > 0)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new Exception("No se pudo insertar el libro");
                }
            }


        }


    }
}
