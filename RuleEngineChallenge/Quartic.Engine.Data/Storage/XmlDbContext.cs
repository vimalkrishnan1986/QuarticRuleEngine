using System;
using Quartic.Engine.Data.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
namespace Quartic.Engine.Data.Storage
{
    public sealed class XmlDbContext<T> : IDbContext<T>
    {
        private string fileName
        {
            get
            {
                return $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{typeof(T).Name}.xml";
            }
        }
        private readonly List<T> _source;

        public XmlDbContext()
        {
            _source = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(fileName));
        }

        private void CreateFile()
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public T Get(Func<T, bool> obj)
        {
            return _source.Single(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return _source.ToList();
        }
    }
}
