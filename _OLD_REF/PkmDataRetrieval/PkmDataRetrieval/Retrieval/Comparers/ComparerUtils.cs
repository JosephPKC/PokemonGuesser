namespace PkmDataRetrieval.Retrieval.Comparers
{
    internal static class ComparerUtils
    {
        public static int? CompareNull<TData>(TData? x, TData? y)
        {
            if (x is null && y is not null)
            {
                return 1;
            }

            if (x is not null && y is null)
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
