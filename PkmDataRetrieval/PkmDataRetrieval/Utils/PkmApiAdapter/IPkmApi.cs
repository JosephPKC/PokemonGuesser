using PkmDataRetrieval.Models;

namespace PkmDataRetrieval.Utils.PkmApiAdapter
{
    public interface IPkmApi
    {
        IEnumerable<PkmResModel>? GetAllPkm();
        PkmModel? GetPkmById(int pId);
    }
}
