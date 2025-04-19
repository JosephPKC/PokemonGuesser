using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    internal class PkmMoveModelComparer : IComparer<PkmMoveModel>
    {
        #region IComparer<PkmMoveModel>
        public int Compare(PkmMoveModel? x, PkmMoveModel? y)
        {
            int? res = ComparerUtils.CompareNull(x, y);
            if (res is not null)
            {
                return res.Value;
            }

            res = ComparerUtils.CompareNull(x!.LevelLearned, y!.LevelLearned); 
            if  (res is not null && res.Value != 0)
            {
                return res.Value;
            }

            res = x!.LevelLearned!.Value.CompareTo(y!.LevelLearned!.Value);
            if (res != 0)
            {
                return res.Value;
            }

            return x!.Name.Name.CompareTo(y!.Name.Name);
        }
        #endregion
    }
}
