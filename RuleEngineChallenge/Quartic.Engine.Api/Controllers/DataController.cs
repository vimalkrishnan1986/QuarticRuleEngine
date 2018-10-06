using Quartic.Engine.Business.Common;
using Quartic.Engine.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using Quartic.Engine.Dto;
using System.Threading.Tasks;
using Quartic.Engine.Api.Common;
namespace Quartic.Engine.Api.Controllers
{
    [RoutePrefix("api/data")]
    public sealed class DataController : QuarticControllerBase
    {
        private readonly IRuleEngineService _ruleEngineService;

        public DataController(ILoggingService loggingService, IRuleEngineService ruleEngineService) : base(loggingService)
        {
            _ruleEngineService = ruleEngineService ?? throw new ArgumentNullException(nameof(ruleEngineService));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] DataContainer container)
        {
            string error = await ValidateContainer(container);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            try
            {
                var res = await _ruleEngineService.Apply(container.Packet.ToMessage(), container.Rule.RuleId);
                return Ok(res.ToPacket());
            }
            catch (Exception ex)
            {
                base.HandleExcpetion(ex);
                return InternalServerError();
            }
        }

        private async Task<string> ValidateContainer(DataContainer dataContainer)
        {
            string message = string.Empty;
            if (dataContainer == null)
            {
                message = $"Value Cannot be Null  {nameof(dataContainer)}";
                return await Task.FromResult(message);
            }

            if (dataContainer.Rule == null)
            {
                message = $"Value Cannot be Null {nameof(dataContainer.Rule)}";
                return await Task.FromResult(message);
            }

            if (dataContainer.Packet == null)
            {
                message = $"Value Cannot be Null {nameof(dataContainer.Packet)}";
                return await Task.FromResult(message);
            }
            return await Task.FromResult(message);
        }
    }
}
