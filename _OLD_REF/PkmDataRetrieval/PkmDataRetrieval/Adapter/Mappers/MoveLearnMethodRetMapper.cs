using PkmApi.Dtos.Move.MoveLearnMethod;

using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class MoveLearnMethodRetMapper
    {
        public static MoveLearnMethodRetModel MapTo(MoveLearnMethodDto pDto)
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
