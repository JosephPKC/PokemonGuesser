namespace PkmApi.Utils
{
    // TODO: Just like the parser, eventually use the UtilsLab log wrapper instead.
    internal class ConsoleLogger : ILogger
    {
        private LogLevel _level = LogLevel.Debug;
        #region ILogger
        public void Debug(object pMessage)
        {
            WriteToConsole(LogLevel.Debug, pMessage);
        }

        public void Error(object pMessage)
        {
            WriteToConsole(LogLevel.Error, pMessage);
        }

        public void Info(object pMessage)
        {
            WriteToConsole(LogLevel.Info, pMessage);
        }
        
        public void SetLogLevel(LogLevel pLevel)
        {
            _level = pLevel;
        }

        public void Warn(object pMessage)
        {
            WriteToConsole(LogLevel.Warn, pMessage);
        }
        #endregion

        private void WriteToConsole(LogLevel pLevel, object pMessage)
        {
            if (!ShouldWriteLog(pLevel))
            {
                return;
            }

            Console.WriteLine($"{pLevel.ToString().ToUpper()}: {pMessage}");
        }

        private bool ShouldWriteLog(LogLevel pLevel)
        {
            return pLevel >= _level;
        }
    }
}
