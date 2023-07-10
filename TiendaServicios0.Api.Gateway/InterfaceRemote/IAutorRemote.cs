using TiendaServicios0.Api.Gateway.ModelsRemote;

namespace TiendaServicios0.Api.Gateway.InterfaceRemote
{
    public interface IAutorRemote
    {
        Task<(bool resultado, AutorRemote autor, string Error)> GetAutor(Guid AutorId);
    }
}
