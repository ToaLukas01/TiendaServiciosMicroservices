namespace TiendaServicios0.Api.Gateway.ModelsRemote
{
    public class LibroRemote
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }

        // esta propiedad representa los datos del autor al cual corresponde el GuidId de AutorLibro, del libro
        public AutorRemote AutorData { get; set; }
    }
}
