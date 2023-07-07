using System.Collections;

namespace TiendaServicios0.Api.CarritoCompras.Models
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }

        // esta propiedad representa la relacion 1 a muchos entre unsa Secion del carrido y cada detalle
        public ICollection<CarritoDetalle> ListaDetalle { get; set; }
    }
}
