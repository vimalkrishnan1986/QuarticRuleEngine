using System;
using System.Collections.Generic;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Logging;
using Quartic.Engine.Dto;
using System.Threading.Tasks;
using Quartic.Engine.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Quartic.Engine.Business.Enums;

namespace Quartic.Engine.Api.Controllers
{
    [Route("api/data")]
    public sealed class DataController : QuarticControllerBase
    {
        private readonly IRuleEngineService _ruleEngineService;

        public DataController(ILoggingService loggingService, IRuleEngineService ruleEngineService) : base(loggingService)
        {
            _ruleEngineService = ruleEngineService ?? throw new ArgumentNullException(nameof(ruleEngineService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromHeader]Guid correlationId)
        {
            LoggingService.Log($"Request has been recieved for correlationId {correlationId}");

            return await Task.FromResult(Ok(new DataContainer
            {
                Packet = new List<Packet>()
                { new Packet
                {
                    DataType = (int)DataTypes.Int,
                    Value = "1100",
                    Signal = "Al"
                }
                }
            }));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DataContainer container)
        {
            string error = await ValidateContainer(container);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            try
            {
                var res = await _ruleEngineService.Apply(container.Packet.ToMessages(), container.RuleId);
                return Ok(res.ToPackets());
            }
            catch (Exception ex)
            {
                base.HandleExcpetion(ex);
                throw;
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

            if (dataContainer.RuleId <= 0)
            {
                message = $" {nameof(dataContainer.RuleId)} Should be a valid integer >0 ";
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
