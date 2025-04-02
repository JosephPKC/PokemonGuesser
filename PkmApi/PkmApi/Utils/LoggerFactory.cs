namespace PkmApi.Utils
{
    public static class LoggerFactory
    {
        public static ILogger BuildConsoleLogger()
        {
            return new ConsoleLogger();
        }
    }
}
