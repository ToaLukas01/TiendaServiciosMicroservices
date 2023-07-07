using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios0.Api.Libros.Test
{
    public class AsyncEnumerable<Generico> : EnumerableQuery<Generico>, IAsyncEnumerable<Generico>, IQueryable<Generico>
    {
        public AsyncEnumerable(IEnumerable<Generico> enumerable): base(enumerable)
        {
        }

        public AsyncEnumerable(Expression expression): base(expression)
        {
        }
        public IAsyncEnumerator<Generico> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<Generico>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider { get { return new AsyncQueryProvider<Generico>(this); } }
    }
}
