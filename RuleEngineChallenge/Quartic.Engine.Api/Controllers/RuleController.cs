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

        public async Task<IActionResult> Post([FromBody] Rule rule)
        {
            throw new NotImplementedException();
        }
    }
}
