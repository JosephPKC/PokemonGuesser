using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    internal class PkmMoveModelComparer(PkmMoveModelComparer.PkmMoveComparerTypes pComparerType = PkmMoveModelComparer.PkmMoveComparerTypes.Default) : IComparer<PkmMoveModel>
    {
        public enum PkmMoveComparerTypes
        {
            Default,
            ByLevel,
            ByMachine
        }

        public PkmMoveComparerTypes ComparerType { get; set; } = pComparerType;

        #region IComparer<PkmMoveModel>
        public int Compare(PkmMoveModel? x, PkmMoveModel? y)
        {
            int? res = ComparerUtils.CompareNull(x, y);
            if (res is not null)
            {
                return res.Value;
            }

            return ComparerType switch
            {
                PkmMoveComparerTypes.ByLevel => CompareByLevel(x!, y!),
                PkmMoveComparerTypes.ByMachine => CompareByMachine(x!, y!),
                _ => CompareDefault(x!, y!),
            };
        }
        #endregion

        private static int CompareDefault(PkmMoveModel x, PkmMoveModel y)
        {
            return x!.Name.Name.CompareTo(y!.Name.Name);
        }

        private static int CompareByLevel(PkmMoveModel x, PkmMoveModel y)
        {
            int? res = ComparerUtils.CompareNull(x.LevelLearned, y.LevelLearned);
            if (res is not null)
            {
                return res.Value;
            }

            return x.LevelLearned!.Value.CompareTo(y.LevelLearned!.Value);
        }

        private static int CompareByMachine(PkmMoveModel x, PkmMoveModel y)
        {
            int? res = ComparerUtils.CompareNull(x.MachineName, y.MachineName);
            if (res is not null)
            {
                return res.Value;
            }

            return x.MachineName!.CompareTo(y.MachineName!);
        }
    }
}
