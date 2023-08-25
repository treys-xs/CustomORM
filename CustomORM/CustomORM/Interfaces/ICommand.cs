using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Interfaces
{
    public interface ICommand : IAsyncDisposable
    {
        Task<IDataReader> ExecuteReaderAsync(CancellationToken cancellationToken);
    }
}
