using PkmApi;

namespace PkmDataRetrieval.Adapter.PkmApi
{
    public interface IPkmApiLibFactory
    {
        IPkmApi CreatePkmApi();
    }
}
