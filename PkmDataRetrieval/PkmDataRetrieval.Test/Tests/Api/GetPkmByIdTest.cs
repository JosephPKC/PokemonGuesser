using Microsoft.AspNetCore.Mvc;

using FluentAssertions;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Api;
using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Utils.Cache;

using PkmDataRetrieval.Test.Fakes.TestCacheHandler;

namespace PkmDataRetrieval.Test.Tests.Api
{
    public class GetPkmByIdTest
    {
        [Fact]
        public void GetPkmById_ValidId_Return200Ok()
        {
            int genId = 9;  //  Scarlet/Violet
            int pkmId = 1;  //  Bulbasaur
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway();
            ICacheHandler cache = new TestCacheHandler();
            IDataRetrieval retrieval = DataRetrievalFactory.CreateDataRetriever(gateway, cache, genId);
            DataRetrievalController api = new(retrieval);

            int expected = 200;
            OkObjectResult result = (OkObjectResult)api.GetPkmById(pkmId);
            PkmModel? actual = result.Value as PkmModel;

            result.StatusCode.Should().Be(expected);
            actual.Should().NotBeNull();
            
            actual.Id.Should().Be(pkmId);
            actual.Name.Name.Should().Be("Bulbasaur");
            actual.SpriteUrl.Should().NotBe(string.Empty);

            Assert.True(actual.Abilities.Any());
            Assert.True(actual.Moves.Any());
            Assert.True(actual.OldMoves.Any());
            Assert.True(actual.Types.Any());
        }

        [Fact]
        public void GetPkmById_InvalidId_Return404NotFound()
        {
            int genId = 9;  //  Scarlet/Violet
            int pkmId = 0;
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway();
            ICacheHandler cache = new TestCacheHandler();
            IDataRetrieval retrieval = DataRetrievalFactory.CreateDataRetriever(gateway, cache, genId);
            DataRetrievalController api = new(retrieval);

            int expected = 404;
            NotFoundObjectResult result = (NotFoundObjectResult)api.GetPkmById(pkmId);

            result.StatusCode.Should().Be(expected);
        }
    }
}
