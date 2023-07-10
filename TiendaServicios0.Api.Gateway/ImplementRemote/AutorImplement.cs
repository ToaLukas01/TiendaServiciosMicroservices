using System.Text.Json;
using TiendaServicios0.Api.Gateway.InterfaceRemote;
using TiendaServicios0.Api.Gateway.ModelsRemote;

namespace TiendaServicios0.Api.Gateway.ImplementRemote
{
    public class AutorImplement : IAutorRemote
    {
        private readonly IHttpClientFactory _httClient;
        private readonly ILogger<AutorRemote> _logget;

        private AutorImplement(IHttpClientFactory httClient, ILogger<AutorRemote> logget)
        {
            _httClient = httClient;
            _logget = logget;
        }

        public async Task<(bool resultado, AutorRemote autor, string Error)> GetAutor(Guid AutorId)
        {
            try
            {
                var client = _httClient.CreateClient("AutorService");
                var response = await client.GetAsync($"/Autor/{AutorId}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    // <A que lo queremos Parcear> (Lo que queremos Deserializar, Opciones de configuracion)
                    var result = JsonSerializer.Deserialize<AutorRemote>(data, options);
                    return (true, result, null);
                }
                else
                {
                    return (false, null, response.ReasonPhrase);
                }
            }
            catch(Exception ex)
            {
                _logget.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
