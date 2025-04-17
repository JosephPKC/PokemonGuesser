using PkmDataRetrieval.Api.Models.Generation;
using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Api
{
    public interface IDataRetrieval
    {
        PkmAllModel? GetAllPkm();
        GenModel? GetCurrentGen();
        PkmModel? GetPkmById(int pId);
    }
}
