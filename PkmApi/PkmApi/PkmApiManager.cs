using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.Version;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Item.Item;
using PkmApi.Dtos.Machine.Machine;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;
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
        public IEndpointHandler<FormDto> Form { get; init; } = BuildEndpointHandler<FormDto>("pokemon-form", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<GenerationDto> Generation { get; init; } = BuildEndpointHandler<GenerationDto>("generation", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<ItemDto> Item { get; init; } = BuildEndpointHandler<ItemDto>("item", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<MachineDto> Machine { get; init; } = BuildEndpointHandler<MachineDto>("machine", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<MoveDto> Move { get; init; } = BuildEndpointHandler<MoveDto>("move", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<MoveDamageClassDto> MoveDamageClass { get; init; } = BuildEndpointHandler<MoveDamageClassDto>("move-damage-class", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<MoveLearnMethodDto> MoveLearnMethod { get; init; } = BuildEndpointHandler<MoveLearnMethodDto>("move-learn-method", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<PokedexDto> Pokedex { get; init; } = BuildEndpointHandler<PokedexDto>("pokedex", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<PkmDto> Pokemon { get; init; } = BuildEndpointHandler<PkmDto>("pokemon", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        public IEndpointHandler<SpeciesDto> Species { get; init; } = BuildEndpointHandler<SpeciesDto>("pokemon-species", pVersion, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
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
