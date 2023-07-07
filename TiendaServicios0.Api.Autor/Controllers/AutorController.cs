using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios0.Api.Autor.Aplication;
using TiendaServicios0.Api.Autor.Models;

namespace TiendaServicios0.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearAutor(NuevoAutor.EjecutarAutor data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAutores()
        {
            return await _mediator.Send(new ConsultarAutor.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> DetalleAutor(string id)
        {
            return await _mediator.Send(new FiltroAutores.DetalleAutor{ AutorGuid = id });
        }
    }
}
