using System;

namespace Business.CCS
{
    public class DatabeseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Databese Loglandı");
        }
    }
}
