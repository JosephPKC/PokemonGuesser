using PkmDataRetrieval.Retrieval.Models;

namespace PkmDataRetrieval.Retrieval
{
    public interface IPkmGateway
    {
        IEnumerable<BasicRetModel>? GetAll<TModel>() where TModel : BaseRetModel;
        TModel? GetById<TModel>(int pId) where TModel : BaseRetModel;
    }
}
