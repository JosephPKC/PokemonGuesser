using PkmApi.Dtos.Pokemon.Type;

using PkmDataRetrieval.Retrieval.Models.Type;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class TypeRetMapper
    {
        public static TypeRetModel MapTo(TypeDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = pDto.Name,
                Names = RetMapperUtils.GetNames(pDto.Names)
            };
        }
    }
}
