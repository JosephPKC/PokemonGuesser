using PkmWebApi.TestData;

namespace PkmWebApi.Services.DataService
{
    public interface IPkmDataApi
    {
        PkmAllApiModel? GetAllPkm();
        PkmApiModel? GetPkmById(int pId);
    }
}
