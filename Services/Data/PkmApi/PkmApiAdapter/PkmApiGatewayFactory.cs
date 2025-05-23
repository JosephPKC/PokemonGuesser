using PkmApi;

namespace Data.PkmApi.PkmApiAdapter;
public static class PkmApiGatewayFactory
{
    public static IPkmApiGateway CreateGateway()
    {
        return new PkmApiAdapter();
    }

    public static IPkmApiGateway CreateGateway(LogWrapper.Loggers.ILoggerFactory pLogFactory)
    {
        return new PkmApiAdapter(pLogFactory);
    }

    public static IPkmApiGateway CreateGateway(IPkmApi pPkmApi)
    {
        return new PkmApiAdapter(pPkmApi);
    }
}
