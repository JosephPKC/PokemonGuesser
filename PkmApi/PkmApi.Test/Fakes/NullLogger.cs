using PkmApi.Utils;

namespace PkmApi.Test.Fakes
{
    internal class NullLogger : ILogger
    {
        #region ILogger
        public void Debug(object pMessage)
        {
            //  Do Nothing
        }

        public void Error(object pMessage)
        {
            //  Do Nothing
        }

        public void Info(object pMessage)
        {
            //  Do Nothing
        }

        public void SetLogLevel(LogLevel pLevel)
        {
            //  Do Nothing
        }

        public void Warn(object pMessage)
        {
            //  Do Nothing
        }
        #endregion
    }
}
