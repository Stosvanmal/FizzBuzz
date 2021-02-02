using FizzBuzz.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Worker
{
    public class WorkerFizz : BackgroundService
    {
        private readonly ILogger<WorkerFizz> _logger;
        private readonly IFizzBuzzService fizzBuzzService;
        private readonly IConfiguration configuration;

        public WorkerFizz(ILogger<WorkerFizz> logger, IFizzBuzzService fizzBuzzService, IConfiguration configuration)
        {
            _logger = logger;
            this.fizzBuzzService = fizzBuzzService;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                int times = this.configuration.GetValue<int>("times");
                var res = await this.fizzBuzzService.Generate(6, times);
                Console.WriteLine(JsonConvert.SerializeObject(res,new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
