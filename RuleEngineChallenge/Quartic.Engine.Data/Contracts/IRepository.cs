using System;
using System.Collections.Generic;
using System.Text;

namespace Quartic.Engine.Data.Contracts
{
    public interface IRepository<T>:IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(Func<T,bool> obj);
    }
}
