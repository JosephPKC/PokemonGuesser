using System.Reflection.Metadata.Ecma335;
using PkmApi.DTOs.Shared;
using PkmApi.Utils;

namespace PkmApi.Test.Fakes
{
    internal class NullGetter : IApiGetter
    {
        private readonly string _testBasicDTOJson1 = @"{
            ""name"": ""testName"", 
            ""id"": 1, 
            ""sub-data"": [
                {
                    ""value"": ""testVal1""
                },
                {
                    ""value"": ""testVal2""
                },
                {
                    ""value"": ""testVal3""
                }
            ]
        }";

        private readonly string _testResLiDTOJson1 = @"{
            ""count"": 10, 
            ""next"": ""test-next"",
            ""previous"": ""test-previous"",
            ""results"": [
                {
                    ""name"": ""test-res1"",
                    ""url"": ""test-res1-url""
                },
                {
                    ""name"": ""test-res2"",
                    ""url"": ""test-res2-url""
                }
            ]
        }";

        public Type DTOJsonType { get; set; } = typeof(BasicTestDTO);

        #region IApiGetter
        public string Get(string pUri)
        {
            return DTOJsonType switch
            {
                Type obj when obj == typeof(BasicTestDTO) => _testBasicDTOJson1,
                Type obj when obj == typeof(ResLiDTO) => _testResLiDTOJson1,
                _ => ""
            };
        }

        public Task<string> GetAsync(string pUri)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
