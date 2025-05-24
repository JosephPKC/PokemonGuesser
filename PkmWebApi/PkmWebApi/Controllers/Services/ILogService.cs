namespace PkmWebApi.Controllers.Services
{
    public interface ILogService<TType>
    {
        LogLevel LogLevel { get; set; }

        void Critical(object? pMessage);
        void Debug(object? pMessage);
        void Error(object? pMessage);
        void Info(object? pMessage);
        void Warn(object? pMessage);
    }
}
