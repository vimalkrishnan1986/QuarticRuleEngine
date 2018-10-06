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
            if (container == null)
            {
                return BadRequest();
            }
            try
            {
                var res = await _ruleEngineService.Apply(container.Packet.ToMessage());
                return Ok(res.ToPacket());
            }
            catch (Exception ex)
            {
                base.HandleExcpetion(ex);
                return InternalServerError();
            }
        }
    }
}
