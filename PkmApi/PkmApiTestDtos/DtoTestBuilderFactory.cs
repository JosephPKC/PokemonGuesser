using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
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
using PkmApi.Dtos.Utility;

using PkmApiTestDtos.Game;
using PkmApiTestDtos.Item;
using PkmApiTestDtos.Machine;
using PkmApiTestDtos.Move;
using PkmApiTestDtos.Pokemon;
using PkmApiTestDtos.Utility;

namespace PkmApiTestDtos
{
    public static class DtoTestBuilderFactory
    {
        /// <summary>
        /// Creates a standard TestDtoBuilder based on the TDto type.
        /// If there are any extension methods you want to access, simply cast it to the actual concrete Builder type.
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public static IDtoTestBuilder<TDto>? CreateDtoTestBuilder<TDto>() where TDto : IPkmApiDto
        {
            return typeof(TDto).Name switch
            {
                nameof(AbilityDto) => new AbilityDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(FormDto) => new FormDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(GenerationDto) => new GenerationDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(ItemDto) => new ItemDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(MachineDto) => new MachineDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(MoveDto) => new MoveDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(MoveDamageClassDto) => new MoveDamageClassDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(MoveLearnMethodDto) => new MoveLearnMethodDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(PokedexDto) => new PokedexDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(PkmDto) => new PkmDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(SpeciesDto) => new SpeciesDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(TypeDto) => new TypeDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(VersionGroupDto) => new VersionGroupDtoTestBuilder() as IDtoTestBuilder<TDto>,
                nameof(ResLiDto) => new ResLiDtoTestBuilder() as IDtoTestBuilder<TDto>,
                _ => null
            };
        }
    }
}
