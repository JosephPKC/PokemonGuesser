using Data.Models;
using Data.Models.Api;
using Data.Models.Api.Pokemon;
using Data.Models.Form;
using Data.Models.Move;
using Data.Models.MoveDamageClass;
using Data.Models.Pokemon;
using Data.Models.Species;
using Data.Models.Type;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.GetModel;
internal class GetPkmByIdController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId, ISet<int> pCurrVersGrpIds, string pKeyPrefix, StaticDataLookUp pStaticData)
: BaseGetModelController(pApi, pCacheHandler, pLog, pCurrGenId, pCurrVersGrpIds, pKeyPrefix, pStaticData)
{
    public PkmApiModel? GetPkmById(int pId)
    {
        string key = $"{_keyPrefix}:{pId}";

        PkmApiModel? getFromApi()
        {
            return GetPkmByIdFromApi(pId);
        }

        return GetApiModel(key, getFromApi);
    }

    private PkmApiModel? GetPkmByIdFromApi(int pId)
    {
        PkmDataModel? pkmModel = GetDataModelById<PkmDataModel>(pId);
        if (pkmModel is null)
        {
            return null;
        }

        string pkmName = GetPkmName(pkmModel);
        List<NameApiModel> pkmTypes = GetPkmTypeNames(pkmModel);

        List<PkmMoveApiModel> moves = [];
        foreach (PkmMoveDataModel pkmMoveModel in pkmModel.Moves)
        {
            AddPkmMoves(moves, pkmMoveModel);
        }
        moves.Sort((x, y) => x.LevelLearned.CompareTo(y.LevelLearned));

        return new()
        {
            Id = pId,
            Name = new()
            {
                Name = pkmName,
                NameKey = pkmModel.NameKey
            },
            SpriteUrl = pkmModel.SpriteFrontDefaultUrl,
            Types = pkmTypes,
            Moves = moves
        };
    }

    #region Get Pkm Name
    private string GetPkmName(PkmDataModel pPkmModel)
    {
        string? pkmName;
        if (IsAltForm(pPkmModel.NameKey))
        {
            pkmName = GetAltFormPkmName(pPkmModel);
        }
        else
        {
            pkmName = GetDefaultPkmName(pPkmModel);
        }

        return pkmName ?? DataUrlUtils.FormatNameKey(pPkmModel.NameKey);
    }

    private string? GetAltFormPkmName(PkmDataModel pPkmModel)
    {
        foreach (string formUrl in pPkmModel.FormResUrls)
        {
            string? pkmName = GetNameFromForm(pPkmModel, formUrl);
            if (pkmName is not null)
            {
                return pkmName;
            }
            else
            {
                log.Warn($"Could not get pkm name from form with url {formUrl}.");
            }
        }

        return null;
    }

    private string? GetNameFromForm(PkmDataModel pPkmModel, string pFormResUrl)
    {
        FormDataModel? formModel = GetDataModelByResUrl<FormDataModel>(pFormResUrl);
        if (formModel is null)
        {
            return null;
        }

        if (pPkmModel.NameKey != formModel.NameKey)
        {
            return null;
        }

        return GetEnLangName(formModel.Names) ?? null;
    }

    private string? GetDefaultPkmName(PkmDataModel pPkmModel)
    {
        SpeciesDataModel? specModel = GetDataModelByResUrl<SpeciesDataModel>(pPkmModel.SpeciesResUrl);
        if (specModel is null)
        {
            return null;
        }

        return GetEnLangName(specModel.Names) ?? null;
    }
    #endregion

    #region Get Flavor Text
    private string? GetFlavorText(List<FlavorTextEntryDataModel> pFlavorTextEntries)
    {
        //  Needs to be en language and latest version group
        IEnumerable<FlavorTextEntryDataModel> pValidTexts = pFlavorTextEntries.Where(IsEntryValid);

        return pValidTexts.Any() ? pValidTexts.First().FlavorTextEntry : null;
    }

    private bool IsEntryValid(FlavorTextEntryDataModel pFlavorTextEntry)
    {
        //  Needs to be 'en' language AND in the current version group
        int? langId = DataUrlUtils.GetIdFromUrl(pFlavorTextEntry.LanguageResUrl);
        if (langId is null || langId.Value != Config.EngLangId)
        {
            return false;
        }

        int? versGrpId = DataUrlUtils.GetIdFromUrl(pFlavorTextEntry.VersionGroupResUrl);
        if (versGrpId is null || !_currVersGrpIds.Contains(versGrpId.Value))
        {
            return false;
        }

        return true;
    }
    #endregion

    #region Get Types
    private List<NameApiModel> GetPkmTypeNames(PkmDataModel pPkmModel)
    {
        List<NameApiModel> typeNames = [];
        foreach (string typeUrl in pPkmModel.TypeResUrls)
        {
            if (!_staticData.Types.TryGetValue(typeUrl, out TypeDataModel? typeModel))
            {
                log.Warn($"Could not get Type with url {typeUrl} from static data cache.");
                continue;
            }

            typeNames.Add(new()
            {
                Name = GetEnLangName(typeModel.Names) ?? DataUrlUtils.FormatNameKey(typeModel.NameKey),
                NameKey = typeModel.NameKey
            });
        }

        return typeNames;
    }

    private NameApiModel? GetMoveTypeName(MoveDataModel pMoveModel)
    {
        if (!_staticData.Types.TryGetValue(pMoveModel.TypeResUrl, out TypeDataModel? typeModel))
        {
            log.Warn($"Could not get Type with url {pMoveModel.TypeResUrl} from static data cache.");
            return null;
        }

        return new()
        {
            Name = GetEnLangName(typeModel.Names) ?? DataUrlUtils.FormatNameKey(typeModel.NameKey),
            NameKey = typeModel.NameKey
        };
    }
    #endregion

    #region Get Moves
    private void AddPkmMoves(List<PkmMoveApiModel> pMoves, PkmMoveDataModel pPkmMoveModel)
    {
        MoveDataModel? moveModel = GetDataModelByResUrl<MoveDataModel>(pPkmMoveModel.ResUrl);
        if (moveModel is null)
        {
            return;
        }

        NameApiModel? moveDmgCl = GetMoveDamageClass(moveModel);
        if (moveDmgCl is null)
        {
            log.Warn($"Could not get move damage class from move with url {pPkmMoveModel.ResUrl}.");
            return;
        }

        NameApiModel? moveType = GetMoveTypeName(moveModel);
        if (moveType is null)
        {
            log.Warn($"Could not get move type from move with url {pPkmMoveModel.ResUrl}.");
            return;
        }

        string? flavorText = GetFlavorText([.. moveModel.FlavorTextEntries]) ?? string.Empty;

        List<PkmMoveVersDataModel>? moveVersions = GetMoveVersions(pPkmMoveModel);
        if (moveVersions is null)
        {
            log.Warn($"Could not get move versions from move with url {pPkmMoveModel.ResUrl}.");
            return;
        }

        foreach (PkmMoveVersDataModel moveVersModel in moveVersions)
        {
            PkmMoveApiModel? pkmMove = GetPkmMove(moveVersModel, moveModel, moveDmgCl, moveType, flavorText);
            if (pkmMove is null)
            {
                log.Warn($"Could not get new pkm move from move with url {pPkmMoveModel.ResUrl}.");
                continue;
            }

            pMoves.Add(pkmMove);
        }
    }

    private PkmMoveApiModel? GetPkmMove(PkmMoveVersDataModel pMoveVersRet, MoveDataModel pMoveModel, NameApiModel pMoveDmgCl, NameApiModel pMoveType, string pFlavorText)
    {
        return new()
        {
            Id = pMoveModel.Id,
            Name = new()
            {
                Name = GetEnLangName(pMoveModel.Names) ?? DataUrlUtils.FormatNameKey(pMoveModel.NameKey),
                NameKey = pMoveModel.NameKey
            },
            DamageClass = pMoveDmgCl,
            MoveType = pMoveType,
            Accuracy = pMoveModel.Accuracy,
            Power = pMoveModel.Power,
            Pp = pMoveModel.Pp,
            LevelLearned = pMoveVersRet.LevelLearnedAt,
            FlavorText = pFlavorText
        };
    }

    private List<PkmMoveVersDataModel>? GetMoveVersions(PkmMoveDataModel pPkmMoveModel)
    {
        List<PkmMoveVersDataModel> moveVersions = [];
        foreach (PkmMoveVersDataModel pkmMoveVersModel in pPkmMoveModel.MoveVersions)
        {
            int? id = DataUrlUtils.GetIdFromUrl(pkmMoveVersModel.MoveLearnMethodResUrl);
            if (id is null)
            {
                continue;
            }
            
            if (id.Value != Config.LevelLearnMethodId)
            {
                // Only level up learn method moves
                continue;
            }

            int? versGrpId = DataUrlUtils.GetIdFromUrl(pkmMoveVersModel.VersionGroupResUrl);
            if (versGrpId is null)
            {
                return null;
            }

            if (!_currVersGrpIds.Contains(versGrpId.Value))
            {
                continue;
            }

            moveVersions.Add(pkmMoveVersModel);
        }

        return moveVersions;
    }

    private NameApiModel? GetMoveDamageClass(MoveDataModel pMoveModel)
    {
        if (!_staticData.MoveDamageClasses.TryGetValue(pMoveModel.DamageClassResUrl, out MoveDamageClassDataModel? moveDmgClRet))
        {
            log.Warn($"Could not get Move Damage Class with url {pMoveModel.DamageClassResUrl} from static data cache.");
            return null;
        }

        return new()
        {
            Name = GetEnLangName(moveDmgClRet.Names) ?? moveDmgClRet.NameKey,
            NameKey = moveDmgClRet.NameKey
        };
    }
    #endregion
}
