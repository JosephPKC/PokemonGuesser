using PkmDataRetrieval.Retrieval.Models;

namespace PkmDataRetrieval.Retrieval
{
    public interface IPkmGateway
    {
        IEnumerable<BasicRetModel>? GetAll<TRet>() where TRet : BaseRetModel;
        TRet? GetById<TRet>(int pId) where TRet : BaseRetModel;
    }
}
