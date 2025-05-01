using Microsoft.AspNetCore.Mvc;

using FluentAssertions;

using LogWrapper.Loggers.Null;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Api;
using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Utils.Caching;

using PkmDataRetrieval.Test.Fakes.TestCacheHandler;

namespace PkmDataRetrieval.Test.Tests.Api
{
    public class GetAllPkmTest
    {
        [Fact]
        public void GetAllPkm_ValidGenId_Return200Ok()
        {
            int genId = 9;  //  Scarlet/Violet
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new NullLoggerFactory());
            ICacheHandler cache = new TestCacheHandler();
            IDataRetrieval retrieval = DataRetrievalFactory.CreateDataRetriever(gateway, cache, genId);
            DataRetrievalController api = new(retrieval);

            int expected = 200;
            OkObjectResult result = (OkObjectResult)api.GetAllPkm();
            PkmAllModel? actual = result.Value as PkmAllModel;

            result.StatusCode.Should().Be(expected);
            actual.Should().NotBeNull();
            Assert.True(actual.Ids.Any());
        }
    }
}
