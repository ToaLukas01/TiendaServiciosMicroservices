namespace TiendaServicios0.Api.Autor.Models
{
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }
        public string Nombre { get; set; }
        public string CentroAcademico { get; set; }
        public DateTime? FechaGrado {  get; set; }

        // esta propiedad la usaremos de "ancla" para saber a que "AutorLibro" pertenece cada instancia del modelo "GradoAcademico"
        public int AutorLibroId { get; set; }

        // y con esta ultima propiedad completamos la relacion "uno a muchos" entre los modelos
        // ya que en este caso un "GradoAcademico" pertenece a un "AutorLibro"
        public AutorLibro AutorLibro { get;set; }

        // propiedad de seguimiento de microservicios que representa una clave primaria de la clase
        public string GradoAcademicoGuid { get; set; }
    }
}
