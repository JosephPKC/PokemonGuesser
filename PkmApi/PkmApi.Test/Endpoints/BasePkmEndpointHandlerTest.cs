using PkmApi.Dtos.Shared;
using PkmApi.Endpoints;

using PkmApi.Test.Fakes;

namespace PkmApi.Test.Endpoints
{
    public class BasePkmEndpointHandlerTest
    {
        #region Setup
        private static IEndpointHandler<BasicTestDto> GetEndpointHandler(Type pDTOTypeToTest)
        {
            NullGetter getter = new()
            {
                DTOJsonType = pDTOTypeToTest
            };
            return EndpointHandlerFactory.BuildEndpointHandler<BasicTestDto>("test-uri/api", "v1", "test-endpoint", getter, new BasicParser(), new NullLogger());
        }
        #endregion

        #region GetById
        [Fact]
        public void Test_GetById_ReturnDeserializedJsonDTO()
        {
            // Arrange
            IEndpointHandler<BasicTestDto> handler = GetEndpointHandler(typeof(BasicTestDto));
            string id = "1";

            BasicTestDto expected = TestValues.Test_BasicTestDto;

            // Act
            BasicTestDto? actual = handler.GetById(id);

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
            IEndpointHandler<BasicTestDto> handler = GetEndpointHandler(typeof(ResLiDto));

            ResLiDto expected = TestValues.Test_ResLiDto;

            // Act
            ResLiDto? actual = handler.GetAll();

            // Assert
            Assert.NotNull(actual);
            Assert.Equivalent(expected, actual);
        }
        #endregion
    }
}
