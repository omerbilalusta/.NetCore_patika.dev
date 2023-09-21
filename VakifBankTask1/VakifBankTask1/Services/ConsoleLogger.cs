namespace VakifBankTask1.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("[Console Logger] - "+ message );
        }
    }
}
