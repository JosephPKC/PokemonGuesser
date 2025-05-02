using Microsoft.AspNetCore.Mvc;

using FluentAssertions;

using LogWrapper;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Api;
using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Utils.Caching;

using PkmDataRetrieval.Test.Fakes.TestCacheHandler;

namespace PkmDataRetrieval.Test.Tests.Api
{
    public class GetCurrentGenTest
    {
        [Fact]
        public void GetCurrentGen_ValidGenId_Return200Ok()
        {
            int genId = 9;  //  Scarlet/Violet
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway(LoggerFacFactory.CreateNullLoggerFactory());
            ICacheHandler cache = new TestCacheHandler();
            IDataRetrieval retrieval = DataRetrievalFactory.CreateDataRetriever(gateway, cache, LoggerFacFactory.CreateNullLoggerFactory(), genId);
            DataRetrievalController api = new(retrieval, LoggerFacFactory.CreateNullLoggerFactory());

            int expected = 200;
            int expGenId = genId;
            string expName = "Generation IX";

            OkObjectResult result = (OkObjectResult)api.GetCurrentGen();
            BasicModel? actual = result.Value as BasicModel;

            result.StatusCode.Should().Be(expected);
            actual.Should().NotBeNull();
            actual.Id.Should().Be(expGenId);
            actual.Name.Name.Should().Be(expName);
        }

        [Fact]
        public void GetCurrentGen_InvalidGenId_Return404NotFound()
        {
            int genId = 0;
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway(LoggerFacFactory.CreateNullLoggerFactory());
            ICacheHandler cache = new TestCacheHandler();
            
            int expected = 404;
            int? actual;
            try
            {
                IDataRetrieval retrieval = DataRetrievalFactory.CreateDataRetriever(gateway, cache, LoggerFacFactory.CreateNullLoggerFactory(), genId);
                DataRetrievalController api = new(retrieval, LoggerFacFactory.CreateNullLoggerFactory());
                NotFoundObjectResult result = (NotFoundObjectResult)api.GetCurrentGen();
                actual = result.StatusCode;
            }
            catch (HttpRequestException ex)
            {
                actual = (int?)ex.StatusCode;
            }
            
            actual.Should().Be(expected);
        }
    }
}
