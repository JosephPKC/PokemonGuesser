using PkmApi;
using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.Version;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;
using PkmApi.Endpoints;
using PkmDataRetrieval.Test.Fakes.TestEndpointHandler;

namespace PkmDataRetrieval.Test.Fakes
{
    internal class TestPkmApi(TestPkmApiConfigs pConfigs) : IPkmApi
    {
        private readonly TestPkmApiConfigs _config = pConfigs;

        #region IPkmApi
        public IEndpointHandler<AbilityDto> Ability
        {
            get
            {
                return new TestEndpointHandler<AbilityDto>(_config);
            }
        }

        public IEndpointHandler<FormDto> Form
        {
            get
            {
                return new TestEndpointHandler<FormDto>(_config);
            }
        }


        public IEndpointHandler<GenerationDto> Generation
        {
            get
            {
                return new TestEndpointHandler<GenerationDto>(_config);
            }
        }


        public IEndpointHandler<MoveDto> Move
        {
            get
            {
                return new TestEndpointHandler<MoveDto>(_config);
            }
        }


        public IEndpointHandler<MoveDamageClassDto> MoveDamageClass
        {
            get
            {
                return new TestEndpointHandler<MoveDamageClassDto>(_config);
            }
        }


        public IEndpointHandler<MoveLearnMethodDto> MoveLearnMethod
        {
            get
            {
                return new TestEndpointHandler<MoveLearnMethodDto>(_config);
            }
        }


        public IEndpointHandler<PokedexDto> Pokedex
        {
            get
            {
                return new TestEndpointHandler<PokedexDto>(_config);
            }
        }


        public IEndpointHandler<PkmDto> Pokemon
        {
            get
            {
                return new TestEndpointHandler<PkmDto>(_config);
            }
        }


        public IEndpointHandler<SpeciesDto> Species
        {
            get
            {
                return new TestEndpointHandler<SpeciesDto>(_config);
            }
        }


        public IEndpointHandler<TypeDto> Type
        {
            get
            {
                return new TestEndpointHandler<TypeDto>(_config);
            }
        }


        public IEndpointHandler<VersionDto> Version
        {
            get
            {
                return new TestEndpointHandler<VersionDto>(_config);
            }
        }


        public IEndpointHandler<VersionGroupDto> VersionGroup
        {
            get
            {
                return new TestEndpointHandler<VersionGroupDto>(_config);
            }
        }

        #endregion
    }
}
