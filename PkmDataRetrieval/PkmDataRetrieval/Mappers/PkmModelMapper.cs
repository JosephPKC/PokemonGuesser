using System.Text.Json;
using PkmApi.DTOs.Pokemon;

using PkmDataRetrieval.Models;

namespace PkmDataRetrieval.Mappers
{
    internal static class PkmModelMapper
    {
        public static PkmModel MapTo(PkmDTO pFrom)
        {
            return new()
            {
                Id = pFrom.Id,
                Name = pFrom.Name,
                Species = pFrom.Species is not null ? pFrom.Species.Name : string.Empty,
                Abilities = GetAbilities(pFrom),
                Moves = GetMoves(pFrom),
                Stats = GetStats(pFrom),
                Types = GetTypes(pFrom),
                SpriteUrl = pFrom.Sprites is not null ? pFrom.Sprites.FrontDefault ?? string.Empty : string.Empty
            };
        }

        private static IEnumerable<PkmAbilityModel> GetAbilities(PkmDTO pFrom)
        {
            if (pFrom.Abilities is null)
            {
                return [];
            }

            ICollection<PkmAbilityModel> modelLi = [];
            foreach (PkmAbilitySDTO sdto in pFrom.Abilities)
            {
                if (sdto.Ability is null)
                {
                    continue;
                }

                modelLi.Add(new()
                {
                    Name = sdto.Ability.Name,
                    IsHidden = sdto.IsHidden ?? false,
                    Slot = sdto.Slot ?? 0
                });
            }

            return modelLi;
        }

        private static IEnumerable<PkmMoveModel> GetMoves(PkmDTO pFrom)
        {
            if (pFrom.Moves is null)
            {
                return [];
            }

            ICollection<PkmMoveModel> modelLi = [];
            foreach (PkmMoveSDTO sdto in pFrom.Moves)
            {
                if (sdto.Move is null)
                {
                    continue;
                }

                PkmMoveVersSDTO? moveVersion = GetLatestMoveVersion(sdto);
                if (moveVersion is null)
                {
                    continue;
                }

                modelLi.Add(new()
                {
                    Name = sdto.Move.Name,
                    LevelLearnedAt = moveVersion.LevelLearnedAt ?? 0,
                    LearnMethod = moveVersion.MoveLearnMethod is not null ? moveVersion.MoveLearnMethod.Name : string.Empty,
                    LatestVersion = moveVersion.VersionGroup is not null ? moveVersion.VersionGroup.Name : string.Empty
                    //  To get Type, we need to do another api call for the Move
                });
            }

            return modelLi;
        }

        private static PkmMoveVersSDTO? GetLatestMoveVersion(PkmMoveSDTO pMove)
        {
            if (pMove.VersionGroupDetails is null)
            {
                return null;
            }

            if (pMove.VersionGroupDetails.Count == 0)
            {
                return null;
            }

            return pMove.VersionGroupDetails[pMove.VersionGroupDetails.Count - 1];
        }

        private static IEnumerable<PkmStatModel> GetStats(PkmDTO pFrom)
        {
            if (pFrom.Stats is null)
            {
                return [];
            }

            ICollection<PkmStatModel> modelLi = [];
            foreach (PkmStatSDTO sdto in pFrom.Stats)
            {
                if (sdto.Stat is null)
                {
                    continue;
                }

                modelLi.Add(new()
                {
                    Stat = sdto.Stat.Name,
                    BaseStat = sdto.BaseStat ?? 0
                });
            }

            return modelLi;
        }

        private static IEnumerable<string> GetTypes(PkmDTO pFrom)
        {
            if (pFrom.Types is null)
            {
                return [];
            }

            ICollection<string> modelLi = [];
            foreach (PkmTypeSDTO sdto in pFrom.Types)
            {
                if (sdto.Type is null)
                {
                    continue;
                }

                modelLi.Add(sdto.Type.Name);
            }

            return modelLi;
        }
    }
}
