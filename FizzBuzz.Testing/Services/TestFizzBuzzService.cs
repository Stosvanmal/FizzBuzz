using FizzBuzz.Business.Repositories;
using FizzBuzz.Business.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Testing.Services
{
    class TestFizzBuzzService
    {
        private IFizzBuzzService service;

        [SetUp]
        public void SetUp()
        {
            var repo =  new Mock<IFileRepository>();
            var logService = new Mock<ILogger<FizzBuzzService>>();
            this.service = new FizzBuzzService(repo.Object, logService.Object);

        }

        [Test]
        public async Task Generate_OK()
        {
            var res = await service.Generate(4, 15);
            Assert.IsTrue(res.Count == 15+1, "True");
        }

    }
}
