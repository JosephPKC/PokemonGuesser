using Data.Models.Api;
using Data.Models.Api.Pokemon;

namespace Data;
public interface IDataManager
{
    PkmAllApiModel? GetAllPkm();
    BasicApiModel? GetCurrentGen();
    PkmApiModel? GetPkmById(int pId);
}
