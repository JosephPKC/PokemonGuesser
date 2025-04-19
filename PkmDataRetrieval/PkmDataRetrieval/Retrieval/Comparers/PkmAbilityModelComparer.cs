using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Retrieval.Comparers
{
    internal class PkmAbilityModelComparer : IComparer<PkmAbilityModel>
    {
        #region IComparer<PkmAbilityModel>
        public int Compare(PkmAbilityModel? x, PkmAbilityModel? y)
        {
            int? res = ComparerUtils.CompareNull(x, y);
            if (res is not null)
            {
                return res.Value;
            }

            return x!.Order.CompareTo(y!.Order);
        }
        #endregion
    }
}
