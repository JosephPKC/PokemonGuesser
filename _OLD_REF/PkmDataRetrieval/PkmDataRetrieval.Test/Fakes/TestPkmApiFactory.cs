using LogWrapper.Loggers;
using PkmApi;
using PkmDataRetrieval.Adapter;

namespace PkmDataRetrieval.Test.Fakes
{
    internal class TestPkmApiFactory : IPkmApiLibFactory
    {
        private readonly TestPkmApiConfigs _configs;

        public TestPkmApiFactory()
        {
            _configs = new();
        }

        public TestPkmApiFactory(TestPkmApiConfigs pConfigs)
        {
            _configs = pConfigs;
        }

        #region IPkmApiLibFactory
        public IPkmApi CreatePkmApi()
        {
            return new TestPkmApi(_configs);
        }

        public IPkmApi CreatePkmApi(ILoggerFactory pLogFactory)
        {
            return new TestPkmApi(_configs);
        }
        #endregion
    }
}
