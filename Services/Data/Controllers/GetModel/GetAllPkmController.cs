using Data.Models;
using Data.Models.Api.Pokemon;
using Data.Models.Pokedex;
using Data.Models.Species;
using Data.Models.VersionGroup;
using Data.PkmApi;
using Data.Utils;

namespace Data.Controllers.GetModel;
internal class GetAllPkmController(IPkmApiGateway pApi, ICacheHandler pCacheHandler, ILog pLog, int pCurrGenId, ISet<int> pCurrVersGrpIds, string pKeyPrefix, StaticDataLookUp pStaticData)
    : BaseGetModelController(pApi, pCacheHandler, pLog, pCurrGenId, pCurrVersGrpIds, pKeyPrefix, pStaticData)
{
    public PkmAllApiModel? GetAllPkm()
    {
        string key = $"{_keyPrefix}:{_currGenId}";
        return GetApiModel(key, GetAllPkmFromApi);
    }

    private PkmAllApiModel? GetAllPkmFromApi()
    {
        HashSet<int> pkmSpecIds = [];
        foreach (int versGrpId in _currVersGrpIds)
        {
            AddAllPkmSpecIdsFromVersionGroup(pkmSpecIds, versGrpId);
        }

        HashSet<int> allPkms = [];
        foreach (int pkmSpecId in pkmSpecIds)
        {
            AddAllPkmIdsFromSpecies(allPkms, pkmSpecId);
        }

        List<int> allPkmLi = [.. allPkms];
        allPkmLi.Sort();

        return new()
        {
            Ids = allPkmLi
        };
    }

    #region Get All PkmSpecies Ids
    private void AddAllPkmSpecIdsFromVersionGroup(HashSet<int> pAllPkmSpecIds, int pVersGrpId)
    {
        VersionGroupDataModel? versGrpModel = GetDataModelById<VersionGroupDataModel>(pVersGrpId);
        if (versGrpModel is null)
        {
            return;
        }

        foreach (string pkDexUrl in versGrpModel.PokedexResUrls)
        {
           AddAllPkmSpecIdsFromPokeDex(pAllPkmSpecIds, pkDexUrl);
        }
    }

    private void AddAllPkmSpecIdsFromPokeDex(HashSet<int> pAllPkmSpecIds, string pPkDexUrl)
    {
        PokedexDataModel? pkDexModel = GetDataModelByResUrl<PokedexDataModel>(pPkDexUrl);
        if (pkDexModel is null)
        {
            return;
        }

        foreach (PokedexPkmEntryDataModel pkmEntry in pkDexModel.PokemonEntries)
        {
            AddPkmSpecIdFromPkmEntry(pAllPkmSpecIds, pkmEntry);
        }
    }

    private static void AddPkmSpecIdFromPkmEntry(HashSet<int> pAllPkmSpecIds, PokedexPkmEntryDataModel pPkmEntry)
    {
        int? pkmSpecId = DataUrlUtils.GetIdFromUrl(pPkmEntry.ResUrl);
        if (pkmSpecId is null)
        {
            return;
        }

        DataUrlUtils.AddIfNotExists(pAllPkmSpecIds, pkmSpecId.Value);
    }
    #endregion

    #region Get All Pkm Ids
    private void AddAllPkmIdsFromSpecies(HashSet<int> pAllPkmIds, int pPkmSpecId)
    {
        SpeciesDataModel? pkmSpecModel = GetDataModelById<SpeciesDataModel>(pPkmSpecId);
        if (pkmSpecModel is null)
        {
            return;
        }

        foreach (SpeciesVarietyDataModel specVar in pkmSpecModel.Varieties)
        {
            AddAllPkmIdsFromPkmVariety(pAllPkmIds, specVar);
        }
    }

    private static void AddAllPkmIdsFromPkmVariety(HashSet<int> pAllPkmIds, SpeciesVarietyDataModel pPkmVariety)
    {
        if (!IsDefaultOrAlt(pPkmVariety))
        {
            return;
        }

        int? pkmId = DataUrlUtils.GetIdFromUrl(pPkmVariety.ResUrl);
        if (pkmId is null)
        {
            return;
        }

        DataUrlUtils.AddIfNotExists(pAllPkmIds, pkmId.Value);
    }

    private static bool IsDefaultOrAlt(SpeciesVarietyDataModel pSpecVar)
    {
        return pSpecVar.IsDefault || IsAltForm(pSpecVar.NameKey);
    }
    #endregion
}
