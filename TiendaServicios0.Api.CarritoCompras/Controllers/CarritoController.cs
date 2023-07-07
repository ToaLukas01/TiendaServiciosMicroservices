using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios0.Api.CarritoCompras.DTO;
using TiendaServicios0.Api.CarritoCompras.Mediador;

namespace TiendaServicios0.Api.CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        // instanciamos al objeto mediador
        private readonly IMediator _mediator;
        public CarritoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearCarrito(NuevoCarrito.CrearCarrito data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoSesionDto>> GetCarritoSesionDetalle(int id)
        {
            return await _mediator.Send(new ConsultarCarrito.ConsultarCarro { CarritoSesionId = id });
        }

    }
}
