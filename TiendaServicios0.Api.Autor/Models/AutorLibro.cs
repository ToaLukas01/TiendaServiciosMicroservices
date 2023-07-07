namespace TiendaServicios0.Api.Autor.Models
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }  

        // aqui con esta propiedad indicamos la relacion entre los distintos modelos
        // en este caso el modelo "AutorLibro" puede poseer uno o mas "GradosAcademico",
        // por eso la propiedad es una lista/colleccion
        public ICollection<GradoAcademico> ListaGradoAcademico { get; set; }

        // esta propiedad es una reprecentacion de una clave primaria, ser aun valor universal
        // que se vera reflejado cuando solicitemos datos de "AutorLibro" desde otro microservicio
        public string AutorLibroGuid { get; set; }
            
    }
}
