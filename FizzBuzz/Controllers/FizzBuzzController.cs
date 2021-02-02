using FizzBuzz.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FizzBuzzController : ControllerBase
    {
        private readonly IFizzBuzzService fizzBuzzService;
        private readonly ILogger<FizzBuzzController> logger;
        private readonly IConfiguration configuration;

        public FizzBuzzController(IFizzBuzzService fizzBuzzService, ILogger<FizzBuzzController> logger, IConfiguration configuration)
        {
            this.fizzBuzzService = fizzBuzzService;
            this.logger = logger;
            this.configuration = configuration;
        }
        /// <summary>
        /// Jugar al FizzBuzz
        /// </summary>
        /// <param name="num"> Numero al azar</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Play/{num}")]
        public async Task<IActionResult> Play(int num)
        {
            try
            {
                int.TryParse(this.configuration["times"], out int times);
                return this.Ok(await this.fizzBuzzService.Generate(num, times));
            }
            catch(Exception e)
            {
                logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Comprueba concurrencia archivo
        /// </summary>
        /// <param name="loops">veces de loop</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Test/{loops}")]
        //Se podria poner en un controlador test...
        public async Task<IActionResult> Test(int loops)
        {
            try
            {
                int.TryParse(this.configuration["times"], out int times);
                List<string> result = new List<string>();
                for (int i = 0; i < loops; i++)
                {
                    result.AddRange(await this.fizzBuzzService.Generate(7, times));
                }                             
                return this.Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }
    }
}
