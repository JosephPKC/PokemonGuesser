namespace Data.Models.Basic
{
    public class BasicLiDataModel : IDataModel
    {
        public IEnumerable<BasicDataModel> Li { get; set; } = [];
    }
}
