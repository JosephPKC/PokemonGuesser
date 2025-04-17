using StackExchange.Redis;

using PkmDataRetrieval.Api;
using PkmDataRetrieval.Api.Models.Generation;
using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Retrieval.Models.Ability;
using PkmDataRetrieval.Retrieval.Models.Form;
using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.Move;
using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Retrieval.Models.Pokedex;
using PkmDataRetrieval.Retrieval.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models.Species;
using PkmDataRetrieval.Retrieval.Models.Type;
using PkmDataRetrieval.Retrieval.Models.VersionGroup;
using PkmDataRetrieval.Retrieval.Shared;
using PkmDataRetrieval.Utils;


//TODO:
//-Change stuff to enum (names are done via front end model, as name presentation is a function of frontend)
//-Break stuff down into helpers and classes
//-Move the cached stuff into its on getby api func, so that they can be cached properly in the retriever (type, damage class, learn method). These should be enums.
namespace PkmDataRetrieval.Retrieval
{
    /// <summary>
    /// The core of this service.
    /// It will get data from the external api and transform that data into something the service api endpoint uses.
    /// </summary>
    /// <param name="pApi"></param>
    /// <param name="pConn"></param>
    internal class PkmDataRetriever: IDataRetrieval
    {
        private readonly IPkmGateway _api;
        private readonly RedisDbHandler _redis;
        private readonly int _currentGenId;
        private readonly HashSet<int> _currentVersGrpIds = [];

        public PkmDataRetriever(IPkmGateway pApi, IConnectionMultiplexer pConn, int pGenId = Config.CurrentGenId)
        {
            _api = pApi;
            _redis = new(pConn, Config.RedisServiceKeyPrefix);
            _currentGenId = pGenId;
            _currentVersGrpIds = GetVersionGroupIds(pGenId);
        }

        private HashSet<int> GetVersionGroupIds(int pGenId)
        {
            GenerationRetModel? genRet = GetRetById<GenerationRetModel>(_currentGenId);
            if (genRet is null)
            {
                //  WARN
                return [];
            }

            HashSet<int> versGrpIds = [];
            foreach (string versGrpUrl in genRet.VersionGroupResUrls)
            {
                int? versGrpId = RetrievalUtils.GetIdFromUrl(versGrpUrl);
                if (versGrpId is null)
                {
                    //  WARN
                    continue;
                }

                versGrpIds.Add(versGrpId.Value);
            }

            return versGrpIds;
        }

        #region IDataRetrieval
        public PkmAllModel? GetAllPkm()
        {
            string key = $"{Config.RedisPkmAllKeyPrefix}:{_currentGenId}";
            PkmAllModel? model = _redis.Get<PkmAllModel>(key);
            if (model is not null)
            {
                return model;
            }

            model = GetAllPkmFromApi();
            if (model is null)
            {
                //  WARN
                return null;
            }

            _redis.Add(key, model);
            return model;
        }

        public GenModel? GetCurrentGen()
        {
            string key = $"{Config.RedisGenByIdKeyPrefix}:{_currentGenId}";
            GenModel? model = _redis.Get<GenModel>(key);
            if (model is not null)
            {
                return model;
            }

            model = GetGenFromApi();
            if (model is null)
            {
                //  WARN
                return null;
            }

            _redis.Add(key, model);
            return model;
        }

        public PkmModel? GetPkmById(int pId)
        {
            string key = $"{Config.RedisPkmByIdKeyPrefix}:{pId}";
            PkmModel? model = _redis.Get<PkmModel>(key);
            if (model is not null)
            {
                return model;
            }

            model = GetPkmFromApi(pId);
            if (model is null)
            {
                //  WARN
                return null;
            }

            //_redis.Add(key, model);
            return model;
        }
        #endregion

        #region GetAllPkm
        private PkmAllModel? GetAllPkmFromApi()
        {
            HashSet<int> pkmSpecIds = [];
            foreach (int versGrpId in _currentVersGrpIds)
            {
                VersionGroupRetModel? versGrpRet = GetRetById<VersionGroupRetModel>(versGrpId);
                if (versGrpRet is null)
                {
                    //  WARN
                    continue;
                }

                foreach (string pkDexUrl in versGrpRet.PokedexResUrls)
                {
                    int? pkdexId = RetrievalUtils.GetIdFromUrl(pkDexUrl);
                    if (pkdexId is null)
                    {
                        //  WARN
                        continue;
                    }

                    PokedexRetModel? pkDexRet = GetRetById<PokedexRetModel>(pkdexId.Value);
                    if (pkDexRet is null)
                    {
                        //  WARN
                        continue;
                    }

                    foreach (PokedexPkmEntryRetModel pkmEntry in pkDexRet.PokemonEntries)
                    {
                        int? pkmSpecId = RetrievalUtils.GetIdFromUrl(pkmEntry.ResUrl);
                        if (pkmSpecId is null)
                        {
                            //  WARN
                            continue;
                        }
                        
                        if (pkmSpecIds.Contains(pkmSpecId.Value))
                        {
                            continue;
                        }

                        pkmSpecIds.Add(pkmSpecId.Value);
                    }
                }
            }

            HashSet<int> allPkms = [];
            foreach (int pkmSpecId in pkmSpecIds)
            {
                SpeciesRetModel? pkmSpecRet = GetRetById<SpeciesRetModel>(pkmSpecId);
                if (pkmSpecRet is null)
                {
                    //  WARN
                    continue;
                }

                foreach (SpeciesVarietyRetModel specVar in pkmSpecRet.Varieties)
                {
                    int? pkmId = RetrievalUtils.GetIdFromUrl(specVar.ResUrl);
                    if (pkmId is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (allPkms.Contains(pkmId.Value))
                    {
                        continue;
                    }

                    if (specVar.IsDefault || IsAltForm(specVar))
                    {
                        allPkms.Add(pkmId.Value);
                    }
                }
            }

            return new()
            {
                Ids = allPkms
            };
        }

        private static bool IsAltForm(SpeciesVarietyRetModel pSpecVar)
        {
            return IsAltForm(pSpecVar.NameKey);
        }
        #endregion

        #region GetGenById
        private GenModel? GetGenFromApi()
        {
            GenerationRetModel? genRet = GetRetById<GenerationRetModel>(_currentGenId);
            if (genRet is null)
            {
                //  WARN
                return null;
            }

            string? enLangName = GetEnLangName(genRet.Names);
            if (enLangName is null)
            {
                //  WARN
                enLangName = string.Empty;
            }

            return new()
            {
                Id = _currentGenId,
                Name = enLangName
            };
        }

        private static string? GetEnLangName(IDictionary<string, string> pNames)
        {
            string pEnLangUrl = RetrievalUtils.GetUrlFromId(Config.EngLangId, "language");
            if (pNames.TryGetValue(pEnLangUrl, out string? value))
            {
                return value;
            }

            return null;
        }
        #endregion

        #region GetPkmById
        private PkmModel? GetPkmFromApi(int pId)
        {
            PkmRetModel? pkmRet = GetRetById<PkmRetModel>(pId);
            if (pkmRet is null)
            {
                //  WARN
                return null;
            }

            string pkmName = string.Empty;
            if (IsAltForm(pkmRet))
            {
                foreach (string formUrl in pkmRet.FormResUrls)
                {
                    int? formId = RetrievalUtils.GetIdFromUrl(formUrl);
                    if (formId is null)
                    {
                        //  WARN
                        continue;
                    }

                    FormRetModel? formRet = GetRetById<FormRetModel>(formId.Value);
                    if (formRet is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (IsFormCorrect(pkmRet, formRet))
                    {
                        string? formName = GetEnLangName(formRet.Names);
                        if (formName is not null)
                        {
                            pkmName = formName;
                            break;
                        }
                        else
                        {
                            //  WARN
                        }
                    }
                }
            }
            else
            {
                int? specId = RetrievalUtils.GetIdFromUrl(pkmRet.SpeciesResUrl);
                if (specId is null)
                {
                    //  WARN
                }
                else
                {
                    SpeciesRetModel? specRet = GetRetById<SpeciesRetModel>(specId.Value);
                    if (specRet is null)
                    {
                        //  WARN
                    }
                    else
                    {
                        string? specName = GetEnLangName(specRet.Names);
                        if (specName is not null)
                        {
                            pkmName = specName;
                        }
                        else
                        {
                            //  WARN
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(pkmName))
            {
                //  Just use the name key instead
                pkmName = pkmRet.NameKey;
            }

            List<PkmAbilityModel> pkmAbilities = [];
            foreach (PkmAbilityRetModel pkmAbilityRet in pkmRet.Abilities)
            {
                int? abilityId = RetrievalUtils.GetIdFromUrl(pkmAbilityRet.ResUrl);
                if (abilityId is null)
                {
                    //  WARN
                    continue;
                }

                AbilityRetModel? abilityRet = GetRetById<AbilityRetModel>(abilityId.Value);
                if (abilityRet is null)
                {
                    //  WARN
                    continue;
                }

                string flavorText = string.Empty;
                foreach (FlavorTextEntryRetModel flavorTextRet in abilityRet.FlavorTextEntries)
                {
                    //  Needs to be en language and latest version group
                    int? langId = RetrievalUtils.GetIdFromUrl(flavorTextRet.LanguageResUrl);
                    if (langId is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (langId.Value != Config.EngLangId)
                    {
                        continue;
                    }

                    int? versGrpId = RetrievalUtils.GetIdFromUrl(flavorTextRet.VersionGroupResUrl);
                    if (versGrpId is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (!_currentVersGrpIds.Contains(versGrpId.Value))
                    {
                        continue;
                    }

                    flavorText = flavorTextRet.FlavorTextEntry;
                }

                string? abilityName = GetEnLangName(abilityRet.Names);
                if (string.IsNullOrWhiteSpace(abilityName))
                {
                    abilityName = abilityRet.NameKey;
                }

                pkmAbilities.Add(new()
                {
                    Id = abilityId.Value,
                    Name = abilityName,
                    IsHidden = pkmAbilityRet.IsHidden,
                    FlavorText = flavorText
                });
            }

            List<string> pkmTypes = [];
            Dictionary<string, string> typeNames = [];
            foreach (string typeUrl in pkmRet.TypeResUrls)
            {

                if (typeNames.ContainsKey(typeUrl))
                {
                    continue;
                }

                int? typeId = RetrievalUtils.GetIdFromUrl(typeUrl);
                if (typeId is null)
                {
                    //  WARN
                    continue;
                }

                TypeRetModel? typeRet = GetRetById<TypeRetModel>(typeId.Value);
                if (typeRet is null)
                {
                    //  WARN
                    continue;
                }

                string? typeName = GetEnLangName(typeRet.Names);
                if (typeName is null)
                {
                    //  WARN
                    typeName = typeRet.NameKey;
                }

                typeNames.Add(typeUrl, typeName);
                pkmTypes.Add(typeName);
            }
           
            Dictionary<string, string> moveDmgClNames = [];
            Dictionary<string, string> moveLearnMethNames = [];

            Dictionary<string, List<PkmMoveModel>> newMoves = [];
            List<PkmOldMoveModel> oldMoves = [];
            foreach (PkmMoveRetModel pkmMoveRet in  pkmRet.Moves)
            {
                Console.WriteLine($"**Parsing move {pkmMoveRet.ResUrl}");
                List<PkmMoveVersRetModel> moveMethods = [];
                foreach (PkmMoveVersRetModel pkmMoveVersRet in pkmMoveRet.MoveVersions)
                {
                    int? versGrpId = RetrievalUtils.GetIdFromUrl(pkmMoveVersRet.VersionGroupResUrl);
                    if (versGrpId is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (!_currentVersGrpIds.Contains(versGrpId.Value))
                    {
                        continue;
                    }

                    if (!moveLearnMethNames.ContainsKey(pkmMoveVersRet.MoveLearnMethodResUrl))
                    {
                        int? moveLearnMethId = RetrievalUtils.GetIdFromUrl(pkmMoveVersRet.MoveLearnMethodResUrl);
                        if (moveLearnMethId is null)
                        {
                            //  WARN
                            continue;
                        }

                        MoveLearnMethodRetModel? moveLearnMethRet = GetRetById<MoveLearnMethodRetModel>(moveLearnMethId.Value);
                        if (moveLearnMethRet is null)
                        {
                            //  WARN
                            continue;
                        }

                        string? moveLearnMethName = GetEnLangName(moveLearnMethRet.Names) ?? moveLearnMethRet.NameKey;

                        moveLearnMethNames.Add(pkmMoveVersRet.MoveLearnMethodResUrl, moveLearnMethName);
                    }

                    Console.WriteLine($"Adding move vers {pkmMoveRet.ResUrl} && {pkmMoveVersRet.MoveLearnMethodResUrl} && {pkmMoveVersRet.VersionGroupResUrl}");
                    moveMethods.Add(pkmMoveVersRet);
                }

                int? moveId = RetrievalUtils.GetIdFromUrl(pkmMoveRet.ResUrl);
                if (moveId is null)
                {
                    //  WARN
                    continue;
                }

                MoveRetModel? moveRet = GetRetById<MoveRetModel>(moveId.Value);
                if (moveRet is null)
                {
                    //  WARN
                    continue;
                }

                if (!moveDmgClNames.ContainsKey(moveRet.DamageClassResUrl))
                {

                    int? moveDmgClId = RetrievalUtils.GetIdFromUrl(moveRet.DamageClassResUrl);
                    if (moveDmgClId is null)
                    {
                        //  WARN
                        continue;
                    }

                    MoveDamageClassRetModel? moveDmgClRet = GetRetById<MoveDamageClassRetModel>(moveDmgClId.Value);
                    if (moveDmgClRet is null)
                    {
                        //  WARN
                        continue;
                    }

                    string? moveDmgClName = GetEnLangName(moveDmgClRet.Names) ?? moveDmgClRet.NameKey;
                    moveDmgClNames.Add(moveRet.DamageClassResUrl, moveDmgClName);
                }

                if (!typeNames.ContainsKey(moveRet.TypeResUrl))
                {
                    Console.WriteLine($"Type res url: {moveRet.TypeResUrl}");
                    int? typeId = RetrievalUtils.GetIdFromUrl(moveRet.TypeResUrl);
                    if (typeId is null)
                    {
                        //  WARN
                        Console.WriteLine($"!!Type, no id");
                        continue;
                    }

                    TypeRetModel? typeRet = GetRetById<TypeRetModel>(typeId.Value);
                    if (typeRet is null)
                    {
                        //  WARN
                        Console.WriteLine($"!!Type, no ret");
                        continue;
                    }

                    string? typeName = GetEnLangName(typeRet.Names);
                    if (typeName is null)
                    {
                        typeName = typeRet.NameKey;
                    }

                    typeNames.Add(moveRet.TypeResUrl, typeName);
                }
                Console.WriteLine($"***{moveId}: Got types");
                string flavorText = string.Empty;
                foreach (FlavorTextEntryRetModel flavorTextRet in moveRet.FlavorTextEntries)
                {
                    //  Needs to be en language and latest version group
                    int? langId = RetrievalUtils.GetIdFromUrl(flavorTextRet.LanguageResUrl);
                    if (langId is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (langId.Value != Config.EngLangId)
                    {
                        continue;
                    }

                    int? versGrpId = RetrievalUtils.GetIdFromUrl(flavorTextRet.VersionGroupResUrl);
                    if (versGrpId is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (!_currentVersGrpIds.Contains(versGrpId.Value))
                    {
                        continue;
                    }

                    flavorText = flavorTextRet.FlavorTextEntry;
                }
                Console.WriteLine($"***{moveId}: Got flavor txt");
                if (moveMethods.Count > 0)
                {
                    //  New Move
                    foreach (PkmMoveVersRetModel moveVersRet in moveMethods)
                    {
                        string moveLearnMethName = moveLearnMethNames[moveVersRet.MoveLearnMethodResUrl];

                        if (!newMoves.ContainsKey(moveLearnMethName))
                        {
                            newMoves.Add(moveLearnMethName, []);
                        }

                        newMoves[moveLearnMethName].Add(new()
                        {
                            Id = moveRet.Id,
                            Name = GetEnLangName(moveRet.Names) ?? moveRet.NameKey,
                            Accuracy = moveRet.Accuracy,
                            Power = moveRet.Power,
                            Pp = moveRet.Pp,
                            DamageClass = moveDmgClNames[moveRet.DamageClassResUrl],
                            FlavorText = flavorText,
                            LearnMethod = moveLearnMethName,
                            LevelLearned = moveVersRet.LevelLearnedAt,
                            MoveType = typeNames[moveRet.TypeResUrl]
                        });
                    }
                }
                else
                {
                    //  Old Move
                    oldMoves.Add(new()
                    {
                        Id = moveRet.Id,
                        Name = GetEnLangName(moveRet.Names) ?? moveRet.NameKey
                    });
                }
            }

            Dictionary<string, IEnumerable<PkmMoveModel>> pkmMoves = [];
            foreach (string key in newMoves.Keys)
            {
                pkmMoves.Add(key, [.. newMoves[key]]);
            }

            return new()
            {
                Id = pId,
                Name = pkmName,
                SpriteUrl = pkmRet.SpriteFrontDefaultUrl,
                Types = pkmTypes,
                Abilities = pkmAbilities,
                Moves = pkmMoves,
                OldMoves = oldMoves
            };
        }

        private static bool IsAltForm(PkmRetModel pPkmRet)
        {
            return IsAltForm(pPkmRet.NameKey);
        }

        private static bool IsFormCorrect(PkmRetModel pPkmRet, FormRetModel pFormRet)
        {
            return pPkmRet.NameKey == pFormRet.NameKey;
        }
        #endregion

        #region Misc
        private TModel? GetRetById<TModel>(int pId) where TModel : BaseRetModel
        {
            string key = $"{GetRetKeyPrefix<TModel>()}:{pId}";
            TModel? model = _redis.Get<TModel>(key);
            if (model is not null)
            {
                return model;
            }

            model = _api.GetById<TModel>(pId);
            if (model is null)
            {
                //  WARN
                return null;
            }

            _redis.Add(key, model);
            return model;
        }

        private static string GetRetKeyPrefix<TModel>() where TModel : BaseRetModel
        {
            return $"{Config.RedisRetKeyPrefix}:{GetRetModelPrefix<TModel>()}";
        }

        private static string GetRetModelPrefix<TModel>() where TModel : BaseRetModel
        {
            return typeof(TModel) switch
            {
                Type model when model == typeof(AbilityRetModel) => "ability",
                Type model when model == typeof(FormRetModel) => "form",
                Type model when model == typeof(GenerationRetModel) => "generation",
                Type model when model == typeof(MoveRetModel) => "move",
                Type model when model == typeof(MoveDamageClassRetModel) => "move-damage-class",
                Type model when model == typeof(MoveLearnMethodRetModel) => "move-learn-method",
                Type model when model == typeof(PokedexRetModel) => "pokedex",
                Type model when model == typeof(PkmRetModel) => "pokemon",
                Type model when model == typeof(SpeciesRetModel) => "species",
                Type model when model == typeof(TypeRetModel) => "type",
                Type model when model == typeof(VersionGroupRetModel) => "version-group",
                _ => string.Empty
            };
        }

        private static bool IsAltForm(string pName)
        {
            //  TODO: Need a better way of determining alt forms.
            //  For now, hardcoded to include all paldean forms only.
            return pName.Contains("PALDEA", StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion




    }
}
