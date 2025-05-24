using PkmWebApi.Models.Refs;

namespace PkmWebApi.Controllers.Services
{
    public interface IDataService
    {
        PkmRefModel GetRandomPkm();
    }
}
