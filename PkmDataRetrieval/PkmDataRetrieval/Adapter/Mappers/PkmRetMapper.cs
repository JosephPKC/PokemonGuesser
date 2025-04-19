using PkmApi.Dtos.Pokemon.Pokemon;

using PkmDataRetrieval.Retrieval.Models.Pokemon;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class PkmRetMapper
    {
        public static PkmRetModel MapTo(PkmDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                SpriteFrontDefaultUrl = pDto.Sprites is not null ? pDto.Sprites.FrontDefault ?? string.Empty : string.Empty,
                SpeciesResUrl = RetMapperUtils.GetUrl(pDto.Species),
                FormResUrls = RetMapperUtils.GetLi(pDto.Forms),
                TypeResUrls = GetTypes(pDto),
                Abilities = GetAbilities(pDto),
                Moves = GetMoves(pDto)
            };
        }

        private static IEnumerable<string> GetTypes(PkmDto pDto)
        {
            static bool isValid(PkmTypeSdto pDto)
            {
                return pDto.Type is not null;
            }

            static string mapTo(PkmTypeSdto pDto)
            {
                return RetMapperUtils.GetUrl(pDto.Type);
            }

            return RetMapperUtils.GetLi(pDto.Types, isValid, mapTo);
        }

        private static IEnumerable<PkmAbilityRetModel> GetAbilities(PkmDto pDto)
        {
            static bool isValid(PkmAbilitySdto pDto)
            {
                return pDto.Ability is not null;
            }

            static PkmAbilityRetModel mapTo(PkmAbilitySdto pDto)
            {
                return new()
                {
                    IsHidden = pDto.IsHidden ?? false,
                    ResUrl = RetMapperUtils.GetUrl(pDto.Ability),
                    Slot = pDto.Slot ?? 0
                };
            }

            return RetMapperUtils.GetLi(pDto.Abilities, isValid, mapTo);
        }

        private static IEnumerable<PkmMoveRetModel> GetMoves(PkmDto pDto)
        {
            static bool isValid(PkmMoveSdto pDto)
            {
                return pDto.Move is not null;
            }

            static PkmMoveRetModel mapTo(PkmMoveSdto pDto)
            {
                return new()
                {
                    MoveVersions = GetMoveVersions(pDto),
                    ResUrl = RetMapperUtils.GetUrl(pDto.Move)
                };
            }

            return RetMapperUtils.GetLi(pDto.Moves, isValid, mapTo);
        }

        private static IEnumerable<PkmMoveVersRetModel> GetMoveVersions(PkmMoveSdto pSdto)
        {
            static bool isValid(PkmMoveVersSdto pDto)
            {
                return pDto.MoveLearnMethod is not null && pDto.VersionGroup is not null;
            }

            static PkmMoveVersRetModel mapTo(PkmMoveVersSdto pDto)
            {
                return new()
                {
                    LevelLearnedAt = pDto.LevelLearnedAt ?? 0,
                    MoveLearnMethodResUrl = RetMapperUtils.GetUrl(pDto.MoveLearnMethod),
                    VersionGroupResUrl = RetMapperUtils.GetUrl(pDto.VersionGroup)
                };
            }

            return RetMapperUtils.GetLi(pSdto.VersionGroupDetails, isValid, mapTo);
        }
    }
}
