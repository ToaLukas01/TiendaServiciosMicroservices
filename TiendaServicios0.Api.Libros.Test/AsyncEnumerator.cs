using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios0.Api.Libros.Test
{
    // toda esta clase esta creada para que represente peticiones asyncronas
    // en las clases de prueba de Test que Emulan bases de datos
    public class AsyncEnumerator<Generico> : IAsyncEnumerator<Generico>
    {
        private readonly IEnumerator<Generico> enumerator;

        public Generico Current => enumerator.Current;

        public AsyncEnumerator(IEnumerator<Generico> enumerator)
        {
            this.enumerator = enumerator ?? throw new ArgumentNullException();
        }

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(enumerator.MoveNext());
        }
    }
}
