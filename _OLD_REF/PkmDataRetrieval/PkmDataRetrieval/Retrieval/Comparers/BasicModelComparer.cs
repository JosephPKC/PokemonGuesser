using PkmDataRetrieval.Api.Models;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    internal class BasicModelComparer : IComparer<BasicModel>
    {
        #region IComparer<BasicModel>
        public int Compare(BasicModel? x, BasicModel? y)
        {
            int? res = ComparerUtils.CompareNull(x, y);
            if (res is not null)
            {
                return res.Value;
            }

            return x!.Name.Name.CompareTo(y!.Name.Name);
        }
        #endregion
    }
}
