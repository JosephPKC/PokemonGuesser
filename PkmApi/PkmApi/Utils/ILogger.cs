namespace PkmApi.Utils
{
    public interface ILogger
    {
        void Debug(object pMessage);
        void Error(object pMessage);
        void Info(object pMessage);
        void SetLogLevel(LogLevel pLevel);
        void Warn(object pMessage);
    }

    //  Organized from lowest level to highest
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }
}
