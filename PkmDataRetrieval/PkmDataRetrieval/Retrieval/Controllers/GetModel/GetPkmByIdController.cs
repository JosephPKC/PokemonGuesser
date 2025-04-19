using StackExchange.Redis;

using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Api.Models.Shared;
using PkmDataRetrieval.Retrieval.Comparers;
using PkmDataRetrieval.Retrieval.Models.Ability;
using PkmDataRetrieval.Retrieval.Models.Form;
using PkmDataRetrieval.Retrieval.Models.Meta;
using PkmDataRetrieval.Retrieval.Models.Move;
using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Retrieval.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models.Shared;
using PkmDataRetrieval.Retrieval.Models.Species;
using PkmDataRetrieval.Retrieval.Models.Type;

namespace PkmDataRetrieval.Retrieval.Controllers.GetModel
{
    internal class GetPkmByIdController(IPkmGateway pApi, IConnectionMultiplexer pConn, KeyPrefixes pKeyPrefixes, CurrentIds pCurrentIds, StaticDataCont pStaticData)
        : BaseGetModelController(pApi, pConn, pKeyPrefixes, pCurrentIds, pStaticData)
    {
        public PkmModel? GetPkmById(int pId)
        {
            string key = $"{_actionKeyPrefix}:{pId}";

            PkmModel? getFromApi()
            {
                return GetPkmByIdFromApi(pId);
            }

            return GetModel(key, getFromApi);
        }

        private PkmModel? GetPkmByIdFromApi(int pId)
        {
            PkmRetModel? pkmRet = GetRetById<PkmRetModel>(pId);
            if (pkmRet is null)
            {
                //  WARN
                return null;
            }

            string pkmName = GetPkmName(pkmRet);
            List<PkmAbilityModel> pkmAbilities = GetAbilities(pkmRet);
            List<NameModel> pkmTypes = GetPkmTypeNames(pkmRet);

            Dictionary<string, List<PkmMoveModel>> pNewMoves = [];
            Dictionary<string, IEnumerable<PkmMoveModel>> pNewMovesFinal = [];
            List<BasicModel> pOldMoves = [];

            foreach (PkmMoveRetModel pkmMoveRet in pkmRet.Moves)
            {
                AddPkmMoves(pNewMoves, pOldMoves, pkmMoveRet);
            }

            foreach (string moveLearnMethod in pNewMoves.Keys)
            {
                //  Sort based on Level
                pNewMoves[moveLearnMethod].Sort(new PkmMoveModelComparer());
                pNewMovesFinal.Add(moveLearnMethod, pNewMoves[moveLearnMethod]);
            }

            pOldMoves.Sort(new BasicModelComparer());

            return new()
            {
                Id = pId,
                Name = new()
                {
                    Name = pkmName,
                    NameKey = pkmRet.NameKey
                },
                SpriteUrl = pkmRet.SpriteFrontDefaultUrl,
                Abilities = pkmAbilities,
                Types = pkmTypes,
                Moves = pNewMovesFinal,
                OldMoves = pOldMoves
            };
        }

        #region Get Pkm Name
        private string GetPkmName(PkmRetModel pPkmRet)
        {
            string? pkmName;
            if (IsAltForm(pPkmRet.NameKey))
            {
                pkmName = GetAltFormPkmName(pPkmRet);
            }
            else
            {
                pkmName = GetDefaultPkmName(pPkmRet);
            }

            return pkmName ?? RetrievalUtils.FormatNameKey(pPkmRet.NameKey);
        }

        private string? GetAltFormPkmName(PkmRetModel pPkmRet)
        {
            foreach (string formUrl in pPkmRet.FormResUrls)
            {
                string? pkmName = GetNameFromForm(pPkmRet, formUrl);
                if (pkmName is not null)
                {
                    return pkmName;
                }
                else
                {
                    //  WARN
                }
            }

            return null;
        }

        private string? GetNameFromForm(PkmRetModel pPkmRet, string pFormResUrl)
        {
            FormRetModel? formRet = GetRetByResUrl<FormRetModel>(pFormResUrl);
            if (formRet is null)
            {
                //  WARN
                return null;
            }

            if (pPkmRet.NameKey != formRet.NameKey)
            {
                return null;
            }

            return GetEnLangName(formRet.Names) ?? null;
        }

        private string? GetDefaultPkmName(PkmRetModel pPkmRet)
        {
            SpeciesRetModel? specRet = GetRetByResUrl<SpeciesRetModel>(pPkmRet.SpeciesResUrl);
            if (specRet is null)
            {
                //  WARN
                return null;
            }

            return GetEnLangName(specRet.Names) ?? null;
        }
        #endregion

        #region Get Abilities
        private List<PkmAbilityModel> GetAbilities(PkmRetModel pPkmRet)
        {
            List<PkmAbilityModel> pkmAbilities = [];
            foreach (PkmAbilityRetModel pkmAbilityRet in pPkmRet.Abilities)
            {
                PkmAbilityModel? pkmAbility = GetPkmAbility(pkmAbilityRet);
                if (pkmAbility is null)
                {
                    //  WARN
                    continue;
                }

                pkmAbilities.Add(pkmAbility);
            }

            pkmAbilities.Sort(new PkmAbilityModelComparer());

            return pkmAbilities;
        }

        private PkmAbilityModel? GetPkmAbility(PkmAbilityRetModel pPkmAbilityRet)
        {
            AbilityRetModel? abilityRet = GetRetByResUrl<AbilityRetModel>(pPkmAbilityRet.ResUrl);
            if (abilityRet is null)
            {
                //  WARN
                return null;
            }

            string flavorText = GetFlavorText([.. abilityRet.FlavorTextEntries]) ?? string.Empty;
            string? abilityName = GetEnLangName(abilityRet.Names) ?? RetrievalUtils.FormatNameKey(abilityRet.NameKey);

            return new()
            {
                Id = abilityRet.Id,
                Name = new()
                {
                    Name = abilityName,
                    NameKey = abilityRet.NameKey
                },
                IsHidden = pPkmAbilityRet.IsHidden,
                FlavorText = flavorText,
                Order = pPkmAbilityRet.Slot
            };
        }
        #endregion

        #region Get Flavor Text
        private string? GetFlavorText(List<FlavorTextEntryRetModel> pFlavorTextEntries)
        {
            //  Needs to be en language and latest version group
            IEnumerable<FlavorTextEntryRetModel> pValidTexts = pFlavorTextEntries.Where(IsEntryValid);

            return pValidTexts.Any() ? pValidTexts.First().FlavorTextEntry : null;
        }

        private bool IsEntryValid(FlavorTextEntryRetModel pFlavorTextEntry)
        {
            //  Needs to be 'en' language AND in the current version group
            int? langId = RetrievalUtils.GetIdFromUrl(pFlavorTextEntry.LanguageResUrl);
            if (langId is null || langId.Value != Config.EngLangId)
            {
                return false;
            }

            int? versGrpId = RetrievalUtils.GetIdFromUrl(pFlavorTextEntry.VersionGroupResUrl);
            if (versGrpId is null || !_currVersGrpIds.Contains(versGrpId.Value))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Get Types
        private List<NameModel> GetPkmTypeNames(PkmRetModel pPkmRet)
        {
            List<NameModel> typeNames = [];
            foreach (string typeUrl in pPkmRet.TypeResUrls)
            {
                if (!_staticData.Types.TryGetValue(typeUrl, out TypeRetModel? typeRet))
                {
                    //  WARN
                    continue;
                }

                typeNames.Add(new()
                {
                    Name = GetEnLangName(typeRet.Names) ?? RetrievalUtils.FormatNameKey(typeRet.NameKey),
                    NameKey = typeRet.NameKey
                });
            }

            return typeNames;
        }

        private NameModel? GetMoveTypeName(MoveRetModel pMoveRet)
        {
            if (!_staticData.Types.TryGetValue(pMoveRet.TypeResUrl, out TypeRetModel? typeRet))
            {
                //  WARN
                return null;
            }

            return new()
            {
                Name = GetEnLangName(typeRet.Names) ?? RetrievalUtils.FormatNameKey(typeRet.NameKey),
                NameKey = typeRet.NameKey
            };
        }
        #endregion

        #region Get Moves
        private void AddPkmMoves(Dictionary<string, List<PkmMoveModel>> pNewMoves, List<BasicModel> pOldMoves, PkmMoveRetModel pkmMoveRet)
        {
            MoveRetModel? moveRet = GetRetByResUrl<MoveRetModel>(pkmMoveRet.ResUrl);
            if (moveRet is null)
            {
                //  WARN
                return;
            }

            NameModel? moveDmgCl = GetMoveDamageClass(moveRet);
            if (moveDmgCl is null)
            {
                //  WARN
                return;
            }

            NameModel? moveType = GetMoveTypeName(moveRet);
            if (moveType is null)
            {
                //  WARN
                return;
            }

            string? flavorText = GetFlavorText([.. moveRet.FlavorTextEntries]) ?? string.Empty;

            List<PkmMoveVersRetModel>? moveVersions = GetMoveVersions(pkmMoveRet);
            if (moveVersions is null)
            {
                //  WARN
                return;
            }

            if (moveVersions.Count > 0)
            {
                //  New Move
                foreach (PkmMoveVersRetModel moveVersRet in moveVersions)
                {
                    PkmMoveModel? pkmMove = GetNewPkmMove(moveVersRet, moveRet, moveDmgCl, moveType, flavorText);
                    if (pkmMove is null)
                    {
                        //  WARN
                        continue;
                    }

                    if (!pNewMoves.TryGetValue(pkmMove.LearnMethod.NameKey, out List<PkmMoveModel>? value))
                    {
                        pNewMoves.Add(pkmMove.LearnMethod.NameKey, [pkmMove]);
                    }
                    else
                    {
                        value.Add(pkmMove);
                    }
                }
            }
            else
            {
                //  Old Move
                pOldMoves.Add(GetOldPkmMove(moveRet));
            }
        }

        private PkmMoveModel? GetNewPkmMove(PkmMoveVersRetModel pMoveVersRet, MoveRetModel pMoveRet, NameModel pMoveDmgCl, NameModel pMoveType, string pFlavorText)
        {
            if (!_staticData.MoveLearnMethods.TryGetValue(pMoveVersRet.MoveLearnMethodResUrl, out MoveLearnMethodRetModel? moveLearnMetRet))
            {
                //  WARN
                return null;
            }

            return new()
            {
                Id = pMoveRet.Id,
                Name = new()
                {
                    Name = GetEnLangName(pMoveRet.Names) ?? RetrievalUtils.FormatNameKey(pMoveRet.NameKey),
                    NameKey = pMoveRet.NameKey
                },
                LearnMethod = new()
                {
                    Name = GetEnLangName(moveLearnMetRet.Names) ?? RetrievalUtils.FormatNameKey(moveLearnMetRet.NameKey),
                    NameKey = moveLearnMetRet.NameKey
                },
                DamageClass = pMoveDmgCl,
                MoveType = pMoveType,
                Accuracy = pMoveRet.Accuracy,
                Power = pMoveRet.Power,
                Pp = pMoveRet.Pp,
                LevelLearned = pMoveVersRet.LevelLearnedAt,
                FlavorText = pFlavorText
            };
        }

        private static BasicModel GetOldPkmMove(MoveRetModel pMoveRet)
        {
            return new()
            {
                Id = pMoveRet.Id,
                Name = new()
                {
                    Name = GetEnLangName(pMoveRet.Names) ?? RetrievalUtils.FormatNameKey(pMoveRet.NameKey),
                    NameKey = pMoveRet.NameKey
                }
            };
        }

        private List<PkmMoveVersRetModel>? GetMoveVersions(PkmMoveRetModel pPkmMoveRet)
        {
            List<PkmMoveVersRetModel> moveVersions = [];
            foreach (PkmMoveVersRetModel pkmMoveVersRet in pPkmMoveRet.MoveVersions)
            {
                if (!_staticData.MoveLearnMethods.ContainsKey(pkmMoveVersRet.MoveLearnMethodResUrl))
                {
                    //  WARN
                    continue;
                }

                int? versGrpId = RetrievalUtils.GetIdFromUrl(pkmMoveVersRet.VersionGroupResUrl);
                if (versGrpId is null)
                {
                    //  WARN
                    return null;
                }

                if (!_currVersGrpIds.Contains(versGrpId.Value))
                {
                    continue;
                }

                MoveLearnMethodRetModel? moveLearnMethRet = GetRetByResUrl<MoveLearnMethodRetModel>(pkmMoveVersRet.MoveLearnMethodResUrl);
                if (moveLearnMethRet is null)
                {
                    //  WARN
                    continue;
                }

                moveVersions.Add(pkmMoveVersRet);
            }

            return moveVersions;
        }

        private NameModel? GetMoveDamageClass(MoveRetModel pMoveRet)
        {
            if (!_staticData.MoveDamageClasses.TryGetValue(pMoveRet.DamageClassResUrl, out MoveDamageClassRetModel? moveDmgClRet))
            {
                //  WARN
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
}
