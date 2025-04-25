using PkmApi;

namespace PkmDataRetrieval.Adapter.PkmApi
{
    internal class PkmApiLibFactory : IPkmApiLibFactory
    {
        #region IPkmApiLibFactory
        public IPkmApi CreatePkmApi()
        {
            return PkmApiFactory.CreatePkmApi();
        }
        #endregion IPkmApiFactory
    }
}
