using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using TiendaServicios0.Api.CarritoCompras.RemoteInterface;
using TiendaServicios0.Api.CarritoCompras.RemoteModels;

namespace TiendaServicios0.Api.CarritoCompras.RemoteServices
{
    // utilizaremos esta clase para implementar los metodos de la interface 
    // de "ILibrosServices" 
    public class LibrosServices: ILibrosServices
    {
        // utilizaremos la interface de HttpClient para poder trabajar con los Endpoints
        private readonly IHttpClientFactory _httpClient;

        //utilizaremos la interface de "Logger" para poder imprimir errores que puedan estar ocurriendo
        // y le tenemos que indicar sobre que clase va a trabajar
        private readonly ILogger<LibrosServices> _logger; 

        public LibrosServices(IHttpClientFactory httpClient, ILogger<LibrosServices> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // ahora debemos implementar el metodo de la interface de la que se hereda la clase
        public async Task<(bool resultado, LibroRemote Libro, string Error)> GetLibro(Guid LibroId)
        {
            try
            {
                // indicamos "("nombre del servicio a consumir")"
                // esta instancia del HttpClient esta creando un nuevo Cliente
                // y esta tomando la URL base de la microservice de libros, indicada en el Program.cs
                var cliente = _httpClient.CreateClient("Libros");

                // utilizamos el objeto "clienete" que hemos creado,
                // para poder invocar el metodo "GetAsync" para invocar un Endpoint
                // (concretamente llamaremos al controller de la Microservice de libros para utilizar uno de sus Endpoints)
                var response = await cliente.GetAsync($"api/Libro/{LibroId}");

                if (response.IsSuccessStatusCode)
                {
                    // guardamos los datos de los la respuesta JSON obtenida anteriormente, en un formato string del json
                    var data = await response.Content.ReadAsStringAsync();

                    // realizamos esta varaible para añadir una propiedad e indicar que no haya problemas con los datos de mayusculas y minusculas
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    // ahora creamso una variable, que represente a la clase con la cual nosotros queremos "Machear" los datos JSON que hemos obtenido
                    // (en este caso sera crear una instancia de "LibroRemote" con los datos del JSON que nos devolvio el Enpoint)
                    // idnicamos "<Tipo de dato al que queremos que se transforme>" ("parametros(contenido, opcionesDeConfiguracion)")
                    var result = JsonSerializer.Deserialize<LibroRemote>(data, options);

                    return (true, result, null);
                }
                else
                {
                    // en caso que haya ocurrido algun error en la peticion del Endpoint se devolvera los siguiente
                    return (false, null, response.ReasonPhrase);
                }
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        
    }
}
