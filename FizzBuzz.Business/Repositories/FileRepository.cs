using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Business.Repositories
{
    public interface IFileRepository
    {
        Task<bool> WriteFile(IList<string> LstResult);
    }
    public class FileRepository : IFileRepository
    {
        private readonly ILogger<FileRepository> logger;
        private readonly string filepath;

        public FileRepository(ILogger<FileRepository> logger)
        {
            this.logger = logger;
            //Podria pasarse por configuración en el appsetting pero no lo pide...
            filepath = Path.Combine(Directory.GetCurrentDirectory(), "Results", "fizzbuzz.txt");
        }
        public async Task<bool> WriteFile(IList<string> LstResult)
        {
            try
            {
                UnicodeEncoding uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(string.Join(",", LstResult));
                FileInfo fileInfo = new FileInfo(filepath);
                bool isInUse = false;
                while (!isInUse)
                {
                    if (!IsFileLocked(fileInfo))
                    {
                        using (FileStream SourceStream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            SourceStream.Seek(0, SeekOrigin.End);
                            await SourceStream.WriteAsync(result, 0, result.Length);
                            isInUse = true;
                        }
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                logger.LogError("Error escritura: " + e.Message, e);
                throw e;
            }
            bool IsFileLocked(FileInfo file)
            {
                try
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }
                }
                catch (IOException)
                {
                    return true;
                }
                return false;
            }
        }
        
    }
}
