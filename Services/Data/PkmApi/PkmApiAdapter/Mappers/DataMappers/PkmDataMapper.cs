using PkmApi.Dtos.Pokemon.Pokemon;

using Data.Models.Pokemon;
using Data.PkmApi.PkmApiAdapter.Mappers;

namespace Data.PkmApi.PkmApiAdapter.Mappers.DataMappers;
internal class PkmDataMapper : BaseDataMapper, IDataMapper<PkmDataModel, PkmDto>
{
    #region IDataMapper<PkmDataModel, PkmDto>
    public PkmDataModel MapTo(PkmDto pDto)
    {
        return new()
        {
            Id = pDto.Id,
            NameKey = pDto.Name,
            SpriteFrontDefaultUrl = pDto.Sprites is not null ? pDto.Sprites.FrontDefault ?? string.Empty : string.Empty,
            SpeciesResUrl = GetUrl(pDto.Species),
            FormResUrls = GetLi(pDto.Forms),
            TypeResUrls = GetTypes(pDto),
            Moves = GetMoves(pDto)
        };
    }
    #endregion

    private static IEnumerable<string> GetTypes(PkmDto pDto)
    {
        static bool isValid(PkmTypeSdto pDto)
        {
            return pDto.Type is not null;
        }

        static string mapTo(PkmTypeSdto pDto)
        {
            return GetUrl(pDto.Type);
        }

        return GetLi(pDto.Types, isValid, mapTo);
    }

    private static IEnumerable<PkmMoveDataModel> GetMoves(PkmDto pDto)
    {
        static bool isValid(PkmMoveSdto pDto)
        {
            return pDto.Move is not null;
        }

        static PkmMoveDataModel mapTo(PkmMoveSdto pDto)
        {
            return new()
            {
                MoveVersions = GetMoveVersions(pDto),
                ResUrl = GetUrl(pDto.Move)
            };
        }

        return GetLi(pDto.Moves, isValid, mapTo);
    }

    private static IEnumerable<PkmMoveVersDataModel> GetMoveVersions(PkmMoveSdto pSdto)
    {
        static bool isValid(PkmMoveVersSdto pDto)
        {
            return pDto.MoveLearnMethod is not null && pDto.VersionGroup is not null;
        }

        static PkmMoveVersDataModel mapTo(PkmMoveVersSdto pDto)
        {
            return new()
            {
                LevelLearnedAt = pDto.LevelLearnedAt ?? 0,
                MoveLearnMethodResUrl = GetUrl(pDto.MoveLearnMethod),
                VersionGroupResUrl = GetUrl(pDto.VersionGroup)
            };
        }

        return GetLi(pSdto.VersionGroupDetails, isValid, mapTo);
    }
}
