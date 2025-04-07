using PkmApi.DTOs.Pokemon;
using PkmApi.Endpoints;

namespace PkmApi
{
    public interface IPkmApi
    {
        IEndpointHandler<PkmDTO> Pokemon { get; }
    }
}
