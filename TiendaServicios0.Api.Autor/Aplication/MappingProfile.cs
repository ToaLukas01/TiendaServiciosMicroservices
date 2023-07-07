using AutoMapper;
using TiendaServicios0.Api.Autor.Models;

namespace TiendaServicios0.Api.Autor.Aplication
{
    public class MappingProfile: Profile
    {
        // esta clase contendra todos los mapeos que necesitaremos realizar
        // entre una clase de EntityFramework y las clases DTO

        public MappingProfile()
        {
            // aqui inicializamos los mapeos que necesitamos

            // aqui indicamos que la clase "AutorLibro" se va a Mapear en la clase "AutorDto"
            CreateMap<AutorLibro, AutorDto>();


        }
    }
}
