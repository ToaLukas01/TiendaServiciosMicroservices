using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios0.Api.Libro.Aplicacion;

namespace TiendaServicios0.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearLibro(NuevoLibro.CrearLibro data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDto>>> ListarLibros()
        {
            return await _mediator.Send(new ConsultarLibros.ConsultarLibreria());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDto>> DetalleLibro(Guid id)
        {
            return await _mediator.Send(new FiltrarLibros.DetalleLibro { LibroId = id});
        }


    }
}
