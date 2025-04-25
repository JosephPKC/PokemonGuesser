using PkmDataRetrieval.Adapter.PkmApi;
using PkmDataRetrieval.Retrieval;

namespace PkmDataRetrieval.Adapter
{
    public static class PkmGatewayFactory
    {
        public static IPkmGateway CreateGateway()
        {
            return new PkmApiAdapter();
        }

        public static IPkmGateway CreateGateway(IPkmApiLibFactory pPkmApiFactory)
        {
            return new PkmApiAdapter(pPkmApiFactory);
        }
    }
}
