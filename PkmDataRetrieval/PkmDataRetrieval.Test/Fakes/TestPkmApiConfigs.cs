using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;
using PkmApi.Dtos.Utility;

namespace PkmDataRetrieval.Test.Fakes
{
    internal class TestPkmApiConfigs
    {
        public ResLiDto? ReturnThisResLiDto { get; set; } = null;
        public Dictionary<Type, IPkmApiDto> ReturnThisDto { get; set; } = [];
    }
}
