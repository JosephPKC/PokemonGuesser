using PkmWebApi.Controllers.Services;

namespace PkmWebApi.Services.LogService
{
    public class LogServiceAdapter<TType>(LogLevel pLevel) : ILogService<TType>
    {
        #region ILogService<TType>
        public LogLevel LogLevel { get; set; } = pLevel;
        public void Critical(object? pMessage)
        {
            Log(LogLevel.Critical, pMessage);
        }

        public void Debug(object? pMessage)
        {
            Log(LogLevel.Debug, pMessage);
        }

        public void Error(object? pMessage)
        {
            Log(LogLevel.Error, pMessage);
        }

        public void Info(object? pMessage)
        {
            Log(LogLevel.Information, pMessage);
        }

        public void Warn(object? pMessage)
        {
            Log(LogLevel.Warning, pMessage);
        }

        #endregion

        private void Log(LogLevel pLevel, object? pMessage)
        {
            if (LogLevel == LogLevel.None || LogLevel > pLevel)
            {
                return;
            }

            Console.WriteLine($"({nameof(TType)}) {pLevel.ToString().ToUpper()}: {pMessage}");
        }
    }
}
