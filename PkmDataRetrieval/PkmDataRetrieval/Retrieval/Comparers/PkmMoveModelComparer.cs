using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    public class PkmMoveModelComparer : BaseComparer<PkmMoveModel>, IComparer<PkmMoveModel>
    {
        #region IComparer<PkmMoveModel>
        public int Compare(PkmMoveModel? x, PkmMoveModel? y)
        {
            int? res = CompareNull(x, y);
            if (res is not null)
            {
                return res.Value;
            }

            res = CompareNull(x!.LevelLearned, y!.LevelLearned); 
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

        private static int? CompareNull(int? x, int? y)
        {
            if (x is null)
            {
                return 1;
            }

            if (y is null)
            {
                return -1;
            }

            if (x is null && y is null)
            {
                return 0;
            }

            return null;
        }

    }
}
