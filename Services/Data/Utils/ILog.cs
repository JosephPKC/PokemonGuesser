namespace Data.Utils
{
    public interface ILog
    {
        LogLevels LogLevel { get; set; }
        void Debug(object pMessage);
        void Error(object pMessage);
        void Info(object pMessage);
        void Warn(object pMessage);
    }

    public enum LogLevels
    {
        Debug,
        Error,
        Info,
        Warn
    }
}
