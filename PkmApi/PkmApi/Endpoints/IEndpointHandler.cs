using PkmApi.DTOs;
using PkmApi.DTOs.Shared;

namespace PkmApi.Endpoints
{
    public interface IEndpointHandler<out TData> where TData : IPkmApiDTO
    {
        TData? GetById(string pId);
        ResLiDTO? GetAll(int pLimit = 20, int pOffset = 0);
    }
}
