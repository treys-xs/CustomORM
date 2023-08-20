using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Interfaces
{
    public interface IConnection : IAsyncDisposable
    {
        Task OpenAsync();
        ICommand CreateCommand(string sql);
    }
}
