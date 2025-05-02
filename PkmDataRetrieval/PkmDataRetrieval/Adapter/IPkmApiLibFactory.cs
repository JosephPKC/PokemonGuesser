using PkmApi;

namespace PkmDataRetrieval.Adapter
{
    public interface IPkmApiLibFactory
    {
        IPkmApi CreatePkmApi();
        IPkmApi CreatePkmApi(LogWrapper.Loggers.ILoggerFactory pLogFactory);
    }
}
