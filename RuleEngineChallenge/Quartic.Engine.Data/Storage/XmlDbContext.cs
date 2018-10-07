using System;
using Quartic.Engine.Data.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
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
                return $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/ {typeof(T).ToString()}.xml";
            }
        }
        private readonly List<T> _source;

        public XmlDbContext()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            _source = (List<T>)serializer.Deserialize(new XmlTextReader(fileName));

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
