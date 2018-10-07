using System.Collections.Generic;
using System.Threading.Tasks;
using Quartic.Engine.Business.Models;

namespace Quartic.Engine.Business.Common
{
    public interface IRuleEngineService
    {
        Task<List<Message>> Apply(List<Message> message, int ruleId);
    }
}
