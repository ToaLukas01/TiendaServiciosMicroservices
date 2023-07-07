namespace TiendaServicios0.Api.CarritoCompras.RemoteModels
{
    public class LibroRemote
    {
        // esta clase modelo representa los datos de "libro"
        // que provienen de manera remota desde la microservice de Libros
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
