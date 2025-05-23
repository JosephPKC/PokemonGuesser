using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Api
{
    public interface IDataRetrieval
    {
        PkmAllModel? GetAllPkm();
        BasicModel? GetCurrentGen();
        PkmModel? GetPkmById(int pId);
    }
}
