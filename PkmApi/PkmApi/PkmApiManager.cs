using PkmApi.Dtos;
using PkmApi.Dtos.Ability;
using PkmApi.Dtos.Move;
using PkmApi.Dtos.Pokemon;
using PkmApi.Dtos.Type;
using PkmApi.Dtos.Version;
using PkmApi.Dtos.VersionGroup;
using PkmApi.Endpoints;
using PkmApi.Utils;

namespace PkmApi
{
    internal class PkmApiManager(
        string pVersion = Config.DefaultApiVersion,
        IApiGetter? pApiGetter = null, IJsonParser? pJsonParser = null, ILogger? pLogger = null,
        ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null) : IPkmApi
    {

        #region IPkmApi
        public IEndpointHandler<AbilityDto> Ability { get; init; } = BuildEndpointHandler<AbilityDto>("ability", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<MoveDto> Move { get; init; } = BuildEndpointHandler<MoveDto>("move", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<PkmDto> Pokemon { get; init; } = BuildEndpointHandler<PkmDto>("pokemon", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<TypeDto> Type { get; init; } = BuildEndpointHandler<TypeDto>("type", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<VersionDto> Version { get; init; } = BuildEndpointHandler<VersionDto>("version", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<VersionGroupDto> VersionGroup { get; init; } = BuildEndpointHandler<VersionGroupDto>("version-group", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        #endregion

        private static IEndpointHandler<TDto> BuildEndpointHandler<TDto>(
            string pName, string pVersion, IApiGetter? pApiGetter, IJsonParser? pJsonParser, ILogger? pLogger, 
            ICacheFactory? pCacheFactory, int? pCacheSizeLimit, int? pCacheLifeInSec
            ) where TDto : class, IPkmApiDto
        {
            return EndpointHandlerFactory.BuildEndpointHandler<TDto>(
                Config.ApiBaseUri, pVersion, pName,
                pApiGetter ?? new HttpGetter(), pJsonParser ?? new JsonParser(), pLogger ?? new ConsoleLogger(),
                pCacheFactory, pCacheSizeLimit, pCacheLifeInSec
            );
        }
    }
}
