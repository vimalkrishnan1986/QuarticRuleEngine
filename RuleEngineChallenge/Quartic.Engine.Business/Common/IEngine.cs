using Quartic.Engine.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quartic.Engine.Business.Common
{
    public interface IEngine
    {
        Task<List<Message>> Apply(List<Message> inputs);
    }
}
