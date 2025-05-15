using PkmDataRetrieval.Retrieval;

namespace PkmDataRetrieval.Adapter
{
    public static class PkmGatewayFactory
    {
        public static IPkmGateway CreateGateway()
        {
            return new PkmApiAdapter();
        }

        public static IPkmGateway CreateGateway(LogWrapper.Loggers.ILoggerFactory pLogFactory)
        {
            return new PkmApiAdapter(pLogFactory);
        }

        public static IPkmGateway CreateGateway(IPkmApiLibFactory pPkmApiFactory)
        {
            return new PkmApiAdapter(pPkmApiFactory);
        }

        public static IPkmGateway CreateGateway(IPkmApiLibFactory pPkmApiFactory, LogWrapper.Loggers.ILoggerFactory pLogFactory)
        {
            return new PkmApiAdapter(pPkmApiFactory, pLogFactory);
        }
    }
}
