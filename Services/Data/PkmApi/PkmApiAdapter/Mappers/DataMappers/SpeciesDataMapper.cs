using PkmApi.Dtos.Pokemon.Species;

using Data.Models.Species;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers
{
    internal class SpeciesDataMapper : BaseDataMapper, IDataMapper<SpeciesDataModel, SpeciesDto>
    {
        #region IDataMapper<SpeciesDataModel, SpeciesDto>
        public SpeciesDataModel MapTo(SpeciesDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Names = GetNames(pDto.Names),
                Varieties = GetPokemonEntries(pDto) 
            };
        }
        #endregion

        private static IEnumerable<SpeciesVarietyDataModel> GetPokemonEntries(SpeciesDto pDto)
        {
            static bool isValid(PkmSpeciesVarietySdto pDto)
            {
                return pDto.Pokemon is not null;
            }

            static SpeciesVarietyDataModel mapTo(PkmSpeciesVarietySdto pDto)
            {
                return new()
                {
                    IsDefault = pDto.IsDefault ?? false,
                    NameKey = pDto.Pokemon is not null ? pDto.Pokemon.Name : string.Empty,
                    ResUrl = GetUrl(pDto.Pokemon)
                };
            }

            return GetLi(pDto.Varieties, isValid, mapTo);
        }
    }
}
