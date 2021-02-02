using FizzBuzz.Business.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Testing.Repositories
{
    class TestFileRepository
    {
        [SetUp]
        public void SetUp()
        {
           
        }

        [Test]
        public void WriteFile_OK()
        {
            IList<string> lst = new List<string> { "fizz", "buzz" };
            var mq = new Mock<IFileRepository>();
            mq.Setup(x => x.WriteFile(lst)).ReturnsAsync(It.IsAny<bool>());
            mq.Verify();
        }
    }
}
