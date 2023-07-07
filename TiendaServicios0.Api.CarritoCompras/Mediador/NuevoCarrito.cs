using MediatR;
using TiendaServicios0.Api.CarritoCompras.Contexto;
using TiendaServicios0.Api.CarritoCompras.Models;

namespace TiendaServicios0.Api.CarritoCompras.Mediador
{
    public class NuevoCarrito
    {
        // creamos las clases que serviran de mediador entre las peticiones que nos manden los controladores
        // sobre los datos que nececitemso consultar en la base de datos
        public class CrearCarrito : IRequest
        {
            public DateTime? FechaCreacionSesion { get; set; }
            
            public List<string> ProductoLista { get; set; }
        }


        public class ManejadorCrearCarrito : IRequestHandler<CrearCarrito>
        {
            // instancioamos a la persistencia del contexto de la base de datos para poder trabajr con ella
            private readonly ContextoCarrito _contexto;

            public ManejadorCrearCarrito(ContextoCarrito contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(CrearCarrito request, CancellationToken cancellationToken)
            {
               // primero creamos la sesion del carrito de compras
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion,
                    //ListaDetalle = (ICollection<CarritoDetalle>)request.ProductoLista,
                };
                _contexto.CarritoSesion.Add(carritoSesion);
                var valueSesion = await _contexto.SaveChangesAsync();

                if(valueSesion == 0)
                {
                    throw new Exception("Errores en la sesion del carrito");
                }

                // si se crea la sesion exitosamente guardamso su Id
                // para asi poder setear la lista de productos que corresponde a este Id de sesion del carrito
                int id_Sesion = carritoSesion.CarritoSesionId;

                // utilizaremso un bucle para que por cada elemento/producto seleccionado, se agregue al detalle
                // de la sesion del carrito
                foreach(var producto in request.ProductoLista)
                {
                    var detalleCarrito = new CarritoDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id_Sesion,
                        ProductoSeleccionado = producto
                    };
                    _contexto.CarritoDetalle.Add(detalleCarrito); ;
                }
                var valueDetalle = await _contexto.SaveChangesAsync();

                if(valueDetalle > 0)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new Exception("Hubo un error en la seleccion de productos");
                }

            }


        }

    }


}
