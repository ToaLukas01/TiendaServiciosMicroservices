using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios0.Api.CarritoCompras.Contexto;
using TiendaServicios0.Api.CarritoCompras.DTO;
using TiendaServicios0.Api.CarritoCompras.RemoteInterface;

namespace TiendaServicios0.Api.CarritoCompras.Mediador
{
    public class ConsultarCarrito
    {
        public class ConsultarCarro: IRequest<CarritoSesionDto>
        {
            // recordar que aqui se escriven los parametros que resive la clase para realizar la consulta
            public int CarritoSesionId { get; set; }
        }

        public class ManejadorConsultarCarro : IRequestHandler<ConsultarCarro, CarritoSesionDto>
        {
            // obtendremos la data del carrito de la base de datos,
            // para ello debemos comunicarnos con el contexto de la base de datos
            private readonly ContextoCarrito _contexto;

            // utilizaremos la implementacion de la interfas de servicios para consultar el Endpoint de libros
            private readonly ILibrosServices _libroServices;

            public ManejadorConsultarCarro(ContextoCarrito contexto, ILibrosServices libroServices)
            {
                _contexto = contexto;
                _libroServices = libroServices;
            }

            public async Task<CarritoSesionDto> Handle(ConsultarCarro request, CancellationToken cancellationToken)
            {
                // esta es la linea logica para realizar la consulta del Id del carrito a la base de datos
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(c => c.CarritoSesionId == request.CarritoSesionId);

                // aqui obtendremos los Ids de la lista de productos del detalle de la sesion del carrito
                var carritoDetalle = await _contexto.CarritoDetalle.Where(c => c.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                // declaramos un arreglo para guardar cada detalle de cada libro
                var listaCarritoDto = new List<CarritoDetalleDto>();

                // utilizaremos un bucle para recorrer todos los Ids del detalle,
                // y asi utilizar la interfaz de servicio para devolver los datos de cada libro al cual corresponde cada Id
                foreach (var libro in carritoDetalle)
                {
                    // recordar que la propiedad "ProductoSeleccionado" representa el Id del detalle de cada libro (del modelo "CarritoDetalle")
                    var response = await _libroServices.GetLibro(new Guid(libro.ProductoSeleccionado));

                    // consultamos por los datos "resultado"(true o false) de a tupla que devuelve response
                    if (response.resultado)
                    {
                        // creamos una variable que guarde el resultado de la propiedad "Libros" de la tupla
                        var obj_Libro = response.Libro;

                        // como el "obj_Libro" nos esta devolviendo un objeto del modelo "LibroRemote"
                        // devemos utilizar sus datos para crear el objeto DTO del libro que queremos devolver
                        // y agregarlo al arreglo de detalles de libros DTO
                        var libroDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = obj_Libro.Titulo,
                            FechaPublicacion = obj_Libro.FechaPublicacion,
                            LibroId = obj_Libro.LibreriaMaterialId,
                        };
                        listaCarritoDto.Add(libroDetalle);
                    }
                };

                // ahora creamos una instancio del "CarritoSesionDto" que queremos devolver
                var carritoSesionDto = new CarritoSesionDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDto,
                };
                return carritoSesionDto;

            }

        }

    }
}
