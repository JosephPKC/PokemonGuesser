using PkmWebServer.TestData;

namespace PkmWebServer.Services.DataService
{
    public interface IPkmDataApi
    {
        PkmAllApiModel? GetAllPkm();
        PkmApiModel? GetPkmById(int pId);
    }
}
