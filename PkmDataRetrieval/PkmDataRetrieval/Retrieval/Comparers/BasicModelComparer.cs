using PkmDataRetrieval.Api.Models;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    public class BasicModelComparer : BaseComparer<BasicModel>, IComparer<BasicModel>
    {
        #region IComparer<BasicModel>
        public int Compare(BasicModel? x, BasicModel? y)
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
