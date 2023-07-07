using TiendaServicios0.Api.CarritoCompras.RemoteModels;

namespace TiendaServicios0.Api.CarritoCompras.RemoteInterface
{
    // utilizaremos esta interface para implementar el modelo de libros
    // que llega de manera remota desde otra Microservice
    // esta interface nos permitira hacer la operacion de consulta de libros a esta microservice
    // implementando esta interface podremos tener acceso al EndPoint de libros
    public interface ILibrosServices
    {
        // este metodo "GetLibro" recive un parametro del tipo "Guid" para debolver un libro mediante su Id
        // devuelve una tupla indicando (resultado true/false, el libro, algun error ocurrido)
        Task<(bool resultado, LibroRemote Libro, string Error)> GetLibro(Guid LibroId);
    }
}
