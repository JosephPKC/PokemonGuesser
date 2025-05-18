using PkmWebServer.Models.Refs;

namespace PkmWebServer.Controllers.Services
{
    public interface IDataService
    {
        PkmRefModel GetRandomPkm();
    }
}
