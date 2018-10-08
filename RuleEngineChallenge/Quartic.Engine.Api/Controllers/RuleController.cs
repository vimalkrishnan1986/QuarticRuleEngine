using Microsoft.AspNetCore.Mvc;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Dto;
using Quartic.Engine.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quartic.Engine.Api.Controllers
{
    [Route("api/rule")]
    public sealed class RuleController : QuarticControllerBase
    {
        private readonly IRuleBusinessService _ruleBusinessService;

        public RuleController(ILoggingService loggingService, IRuleBusinessService rulebusinessService) : base(loggingService)
        {
            _ruleBusinessService = rulebusinessService ?? throw new ArgumentNullException(nameof(rulebusinessService));
        }

        /// <summary>
        /// This is not implemented dueue to time limitation
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] Guid correlationId, [FromBody] Rule rule)
        {
            throw new NotImplementedException();
        }
    }
}
