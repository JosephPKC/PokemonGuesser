using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    public class PkmOldMoveModelComparer : BaseComparer<PkmOldMoveModel>, IComparer<PkmOldMoveModel>
    {
        #region IComparer<PkmOldMoveModel>
        public int Compare(PkmOldMoveModel? x, PkmOldMoveModel? y)
        {
            int? res = CompareNull(x, y);
            if (res is not null)
            {
                return res.Value;
            }

            return x!.Name.Name.CompareTo(y!.Name.Name);
        }
        #endregion
    }
}
