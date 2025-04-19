using PkmDataRetrieval.Retrieval;

namespace PkmDataRetrieval.Adapter
{
    public static class PkmGatewayFactory
    {
        public static IPkmGateway CreateGateway()
        {
            return new PkmApiAdapter();
        }
    }
}
