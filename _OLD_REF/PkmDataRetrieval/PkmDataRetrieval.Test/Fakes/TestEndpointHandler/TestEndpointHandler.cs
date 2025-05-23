using PkmApi.Dtos;
using PkmApi.Dtos.Utility;
using PkmApi.Endpoints;

namespace PkmDataRetrieval.Test.Fakes.TestEndpointHandler
{
    internal class TestEndpointHandler<TData>(TestPkmApiConfigs pConfigs) : IEndpointHandler<TData> where TData : class, IPkmApiDto
    {
        private readonly TestPkmApiConfigs _configs = pConfigs;

        #region IEndpointHandler<TData>
        public ResLiDto? GetAll(int pLimit = 20, int pOffset = 0)
        {
            return _configs.ReturnThisResLiDto;
        }

        public TData? GetById(string pId)
        {
            if (!_configs.ReturnThisDto.ContainsKey(typeof(TData)))
            {
                return null;
            }

            return _configs.ReturnThisDto[typeof(TData)] as TData;
        }
        #endregion
    }
}
