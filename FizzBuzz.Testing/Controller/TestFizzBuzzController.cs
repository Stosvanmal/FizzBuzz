using FizzBuzz.Business.Services;
using FizzBuzz.Web.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FizzBuzz.Business.Repositories;

namespace FizzBuzz.Testing.Controller
{
    class TestFizzBuzzController
    {
        private FizzBuzzController fizzBuzzController;
        [SetUp]
        public void SetUp()
        {
            var repo = new Mock<IFileRepository>();            
            var logController = new Mock<ILogger<FizzBuzzController>>();
            var logService = new Mock<ILogger<FizzBuzzService>>();
            var inMemorySettings = new Dictionary<string, string> {
                {"times", "10"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var service = new FizzBuzzService(repo.Object,logService.Object);
            this.fizzBuzzController = new FizzBuzzController(service, logController.Object, configuration);

        }

        [Test]
        public async Task Play_OK()
        {
            IActionResult res = await fizzBuzzController.Play(10);
            var contentResult = res as OkObjectResult;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Value);
        }
    }
}
