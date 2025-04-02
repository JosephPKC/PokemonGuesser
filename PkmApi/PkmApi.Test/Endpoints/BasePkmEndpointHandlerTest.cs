using PkmApi.DTOs.Shared;
using PkmApi.Endpoints;

using PkmApi.Test.Fakes;

namespace PkmApi.Test.Endpoints
{
    public class BasePkmEndpointHandlerTest
    {
        #region Setup
        private static IEndpointHandler<BasicTestDTO> GetEndpointHandler(Type pDTOTypeToTest)
        {
            NullGetter getter = new()
            {
                DTOJsonType = pDTOTypeToTest
            };
            return EndpointHandlerFactory.BuildEndpointHandler<BasicTestDTO>("test-uri/api", "v1", "test-endpoint", getter, new BasicParser(), new NullLogger());
        }
        #endregion

        #region GetById
        [Fact]
        public void Test_GetById_ReturnDeserializedJsonDTO()
        {
            // Arrange
            IEndpointHandler<BasicTestDTO> handler = GetEndpointHandler(typeof(BasicTestDTO));
            string id = "1";

            BasicTestDTO expected = TestValues.Test_BasicTestDTO;

            // Act
            BasicTestDTO? actual = handler.GetById(id);

            // Assert
            Assert.NotNull(actual);
            Assert.Equivalent(expected, actual);
        }
        #endregion

        #region GetAll
        [Fact]
        public void Test_GetAll_ReturnDeserializedJsonResLiDTO()
        {
            // Arrange
            IEndpointHandler<BasicTestDTO> handler = GetEndpointHandler(typeof(ResLiDTO));
            string id = "1";

            ResLiDTO expected = TestValues.Test_ResLiDTO;

            // Act
            ResLiDTO? actual = handler.GetAll();

            // Assert
            Assert.NotNull(actual);
            Assert.Equivalent(expected, actual);
        }
        #endregion
    }
}
