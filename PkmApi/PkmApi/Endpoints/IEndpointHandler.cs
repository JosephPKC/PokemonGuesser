using PkmApi.Dtos;
using PkmApi.Dtos.Shared;

namespace PkmApi.Endpoints
{
    public interface IEndpointHandler<out TData> where TData : IPkmApiDto
    {
        TData? GetById(string pId);
        ResLiDto? GetAll(int pLimit = 20, int pOffset = 0);
    }
}
