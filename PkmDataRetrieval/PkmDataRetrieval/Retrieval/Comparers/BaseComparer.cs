namespace PkmDataRetrieval.Retrieval.Comparers
{
    public abstract class BaseComparer<TModel>
    {
        protected int? CompareNull(TModel? x, TModel? y)
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
