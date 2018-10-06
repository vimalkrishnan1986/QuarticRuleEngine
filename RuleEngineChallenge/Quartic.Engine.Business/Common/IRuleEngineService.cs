using Quartic.Engine.Business.Models;
using System.Threading.Tasks;

namespace Quartic.Engine.Business.Common
{
    public interface IRuleEngineService
    {
        Task<Message> Apply(Message message, int ruleId);
    }
}
