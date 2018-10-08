using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartic.Engine.Business.Models;
using Quartic.Engine.Data.Entities;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using Quartic.Engine.Logging;
using Quartic.Engine.Api.Controllers;
using Quartic.Engine.Business.Common;
using Quartic.Engine.Business.Services;
using System.Threading.Tasks;
using Quartic.Engine.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Quartic.Engine.Business.Core;
using Quartic.Engine.Business.Enums;

namespace Quartic.Engine.Api.Test
{
    [TestClass]
    public class DataControllerTest
    {
        private string RuleExpressionFileName
        {
            get
            {
                return $"{ typeof(RuleExpression).Name}.xml";
            }
        }

        private ILoggingService LoggingService
        {
            get
            {
                return Activator.CreateInstance<LoggingService>();
            }
        }

        private IRuleEngineService RuleEngineService
        {
            get
            {
                return new RuleEngineService(LoggingService);
            }
        }


        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(FullPath(RuleExpressionFileName)))
            {
                File.Delete(FullPath(RuleExpressionFileName));
            }
        }

        [TestMethod]
        public async Task DataControllerTest_Post_InvalidData()
        {
            int ruleId = CreateRule();
            var controller = new DataController(LoggingService, RuleEngineService);
            var result = await controller.Post(new DataContainer()
            {
                Packet = new List<Packet>
                {
                    new Packet
                    {
                        DataType=1,
                        Signal="AL",
                        Value="Low"
                    }
                },
                RuleId = 0
            });
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DataControllerTest_Post()
        {
            int ruleId = CreateRule();
            var controller = new DataController(LoggingService, RuleEngineService);
            var result = await controller.Post(new DataContainer()
            {
                Packet = new List<Packet>
                {
                    new Packet
                    {
                        DataType=1,
                        Signal="AL",
                        Value="Low"
                    }
                },
                RuleId = ruleId
            });
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var packets = (List<Packet>)okResult.Value;
            Assert.IsNotNull(packets);
            Assert.IsTrue(packets.Count == 0);
        }

        private string FullPath(string fileName)
        {
            return $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{fileName}";


        }
        private int CreateRule()
        {

            string xpressionstring = JsonConvert.SerializeObject(new FilterExpression
            {
                Field = "Signal",
                Value = "AL",
                Operators = Operators.Equal
            });
            var rules = new List<RuleExpression>()
            { new RuleExpression
            {
                Id = 1,
                Expression = xpressionstring
            } };

            string fullpath = FullPath(RuleExpressionFileName);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
            File.AppendAllText(fullpath, JsonConvert.SerializeObject(rules));
            return rules.First().Id;
        }
    }
}
