using PkmApi.Dtos.Move.MoveDamageClass;

using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class MoveDamageClassRetMapper
    {
        public static MoveDamageClassRetModel MapTo(MoveDamageClassDto pDto)
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
