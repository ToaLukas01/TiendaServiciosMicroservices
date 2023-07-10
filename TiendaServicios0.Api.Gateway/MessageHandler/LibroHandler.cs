using System.Diagnostics;
using System.Text.Json;
using TiendaServicios0.Api.Gateway.InterfaceRemote;
using TiendaServicios0.Api.Gateway.ModelsRemote;

namespace TiendaServicios0.Api.Gateway.MessageHandler
{
   
    public class LibroHandler: DelegatingHandler
    {
        private readonly ILogger<LibroHandler> _logger;
        private readonly IAutorRemote _autorRemote;
        public LibroHandler(ILogger<LibroHandler> logger, IAutorRemote autorRemote)
        {
            _logger = logger;
            _autorRemote = autorRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelationToken)
        {
            var tiempoDemora = Stopwatch.StartNew();
            _logger.LogInformation("Se inicio el requiest");
            var response = await base.SendAsync(request, cancelationToken);

            if (response.IsSuccessStatusCode)
            {
                // si la respuesta fue correcta, guardamos los datos JSON del requiest en un formato string
                // para luego deserializarlos y parcearlos al formato del modelo de Libros
                var data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                // <A que lo queremos Parcear> (Lo que queremos Deserializar, Opciones de configuracion)
                var result_Libro = JsonSerializer.Deserialize<LibroRemote>(data, options);
                
                var responseAutor = await _autorRemote.GetAutor(result_Libro.AutorLibro??Guid.Empty);

                if (responseAutor.resultado)
                {
                    var obj_autor = responseAutor.autor;
                    result_Libro.AutorData = obj_autor;

                    // una vez obtenidos todos los datos del libreo, y de su autor
                    // debemos serializar estos resultados a tipo JSON para poder devolverlos en el response
                    var result_JSON = JsonSerializer.Serialize(result_Libro);
                    response.Content = new StringContent(result_JSON, System.Text.Encoding.UTF8, "application/json");
                }
            }

            _logger.LogInformation($"El proseso demoro {tiempoDemora.ElapsedMilliseconds}es");

            return response;
        }
    }
}
