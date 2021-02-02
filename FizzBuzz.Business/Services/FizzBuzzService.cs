using FizzBuzz.Business.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Business.Services
{
    public interface IFizzBuzzService
    {
        Task<IList<string>> Generate(int startNum, int times);
    }
    public class FizzBuzzService:IFizzBuzzService
    {
        private readonly IFileRepository fileRepository;
        private readonly ILogger<FizzBuzzService> logger;

        public FizzBuzzService(IFileRepository fileRepository, ILogger<FizzBuzzService> logger)
        {
            this.fileRepository = fileRepository;
            this.logger = logger;
        }

        public async Task<IList<string>> Generate(int startNum, int times)
        {
            try
            {
                IList<string> LstResult = new List<string>();
                for (int i = 0; i < times; i++)
                {
                    LstResult.Add(CheckNumber(startNum));
                    startNum++;
                }
                LstResult.Add(DateTime.Now.ToLongTimeString());
                await fileRepository.WriteFile(LstResult);
                return LstResult;

                string CheckNumber(int num)
                {
                    if (num % 3 == 0 && num % 5 == 0)
                    {
                        return "fizzbuzz";
                    }
                    if (num % 3 == 0)
                        return "fizz";
                    if (num % 5 == 0)
                        return "buzz";
                    return num.ToString();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                throw e;
            }
        }

    }
}
