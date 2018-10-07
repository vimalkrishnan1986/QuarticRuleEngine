using System;
using System.Collections.Generic;
using System.Text;
using Quartic.Engine.Data.Contracts;
using Quartic.Engine.Data.Storage;
namespace Quartic.Engine.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private IDbContext<T> _dbContext;
        protected BaseRepository()
        {
            // This needs to be made as injectable. Presently initilizing overe here due to shortage of time
            _dbContext = new XmlDbContext<T>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public T Get(Func<T, bool> obj)
        {
            return _dbContext.Get(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.GetAll();
        }
    }
}
