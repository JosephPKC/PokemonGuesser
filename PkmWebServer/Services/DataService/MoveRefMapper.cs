using PkmWebServer.Models.Refs;
using PkmWebServer.TestData;
using PkmWebServer.Utils;

namespace PkmWebServer.Services.DataService
{
    public static partial class MoveRefMapper
    {
        public static MoveRefModel MapToRef(PkmMoveApiModel pModel)
        {
            return new()
            {
                Id = pModel.Id,
                Name = pModel.Name.Name,
                NameKey = NameCleaner.CleanNameKey(pModel.Name.NameKey),
                LevelLearned = pModel.LevelLearned,
                DamageClass = GetDamageClassHint(pModel),
                Type = GetTypeHint(pModel),
                Stats = GetStatsHint(pModel),
                FlavorText = GetFlavorTextHint(pModel)
            };
        }

        private static HintRefModel GetDamageClassHint(PkmMoveApiModel pModel)
        {
            return new()
            {
                Hint = pModel.DamageClass.Name,
                HintType = Dtos.Hint.HintTypes.DamageClass,
                ScoreCost = Configs.DamageClassHintCost
            };
        }

        private static HintRefModel GetTypeHint(PkmMoveApiModel pModel)
        {
            return new()
            {
                Hint = pModel.MoveType.Name,
                HintType = Dtos.Hint.HintTypes.Type,
                ScoreCost = Configs.TypeHintCost
            };
        }

        private static HintRefModel GetStatsHint(PkmMoveApiModel pModel)
        {
            return new()
            {
                Hint = $"{pModel.Power}/{pModel.Accuracy}/{pModel.Pp}",
                HintType = Dtos.Hint.HintTypes.Stats,
                ScoreCost = Configs.StatHintCost
            };
        }

        private static HintRefModel GetFlavorTextHint(PkmMoveApiModel pModel)
        {
            return new()
            {
                Hint = pModel.FlavorText,
                HintType = Dtos.Hint.HintTypes.FlavorText,
                ScoreCost = Configs.FlavorTextCost
            };
        }
    }
}
