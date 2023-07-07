namespace TiendaServicios0.Api.CarritoCompras.Models
{
    public class CarritoDetalle
    {
        public int CarritoDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; } // esto representa el Id del producto seleccionado

        // aqui creamos el ancla de vinculo que indica que el detalle del carrito pertenece a una sesion del carrito
        // para generar la ClaveForanea
        public int CarritoSesionId { get; set; }
        public CarritoSesion CarritoSesion { get; set; }
    }
}
