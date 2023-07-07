namespace TiendaServicios0.Api.CarritoCompras.DTO
{
    public class CarritoSesionDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
