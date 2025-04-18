    using StackExchange.Redis;

using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models.Pokedex;
using PkmDataRetrieval.Retrieval.Models.Species;
using PkmDataRetrieval.Retrieval.Models.VersionGroup;
using PkmDataRetrieval.Retrieval.Models.Meta;
using PkmDataRetrieval.Retrieval.Builders;

namespace PkmDataRetrieval.Retrieval.Controllers.GetModel
{
    internal class GetAllPkmController(IPkmGateway pApi, IConnectionMultiplexer pConn, KeyPrefixes pKeyPrefixes, CurrentIds pCurrentIds, StaticDataCont pStaticData) 
        : BaseGetModelController(pApi, pConn, pKeyPrefixes, pCurrentIds, pStaticData)
    {
        public PkmAllModel? GetAllPkm()
        {
            string key = $"{_actionKeyPrefix}:{_currGenId}";
            return GetModel(key, GetAllPkmFromApi);
        }

        private PkmAllModel? GetAllPkmFromApi()
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

            return PkmAllModelBuilder.BuildModel(allPkms);
        }

        #region Get All PkmSpecies Ids
        private void AddAllPkmSpecIdsFromVersionGroup(HashSet<int> pAllPkmSpecIds, int pVersGrpId)
        {
            VersionGroupRetModel? versGrpRet = GetRetById<VersionGroupRetModel>(pVersGrpId);
            if (versGrpRet is null)
            {
                //  WARN
                return;
            }

            foreach (string pkDexUrl in versGrpRet.PokedexResUrls)
            {
               AddAllPkmSpecIdsFromPokeDex(pAllPkmSpecIds, pkDexUrl);
            }
        }

        private void AddAllPkmSpecIdsFromPokeDex(HashSet<int> pAllPkmSpecIds, string pPkDexUrl)
        {
            PokedexRetModel? pkDexRet = GetRetByResUrl<PokedexRetModel>(pPkDexUrl);
            if (pkDexRet is null)
            {
                //  WARN
                return;
            }

            foreach (PokedexPkmEntryRetModel pkmEntry in pkDexRet.PokemonEntries)
            {
                AddPkmSpecIdFromPkmEntry(pAllPkmSpecIds, pkmEntry);
            }
        }

        private static void AddPkmSpecIdFromPkmEntry(HashSet<int> pAllPkmSpecIds, PokedexPkmEntryRetModel pPkmEntry)
        {
            int? pkmSpecId = RetrievalUtils.GetIdFromUrl(pPkmEntry.ResUrl);
            if (pkmSpecId is null)
            {
                //  WARN
                return;
            }

            RetrievalUtils.AddIfNotExists(pAllPkmSpecIds, pkmSpecId.Value);
        }
        #endregion

        #region Get All Pkm Ids
        private void AddAllPkmIdsFromSpecies(HashSet<int> pAllPkmIds, int pPkmSpecId)
        {
            SpeciesRetModel? pkmSpecRet = GetRetById<SpeciesRetModel>(pPkmSpecId);
            if (pkmSpecRet is null)
            {
                //  WARN
                return;
            }

            foreach (SpeciesVarietyRetModel specVar in pkmSpecRet.Varieties)
            {
                AddAllPkmIdsFromPkmVariety(pAllPkmIds, specVar);
            }
        }

        private static void AddAllPkmIdsFromPkmVariety(HashSet<int> pAllPkmIds, SpeciesVarietyRetModel pPkmVariety)
        {
            if (!IsDefaultOrAlt(pPkmVariety))
            {
                return;
            }

            int? pkmId = RetrievalUtils.GetIdFromUrl(pPkmVariety.ResUrl);
            if (pkmId is null)
            {
                //  WARN
                return;
            }

            RetrievalUtils.AddIfNotExists(pAllPkmIds, pkmId.Value);
        }

        private static bool IsDefaultOrAlt(SpeciesVarietyRetModel pSpecVar)
        {
            return pSpecVar.IsDefault || IsAltForm(pSpecVar.NameKey);
        }
        #endregion
    }
}
