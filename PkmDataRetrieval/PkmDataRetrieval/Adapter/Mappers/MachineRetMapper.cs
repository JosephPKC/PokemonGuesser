using PkmApi.Dtos.Machine.Machine;

using PkmDataRetrieval.Retrieval.Models.Machine;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class MachineRetMapper
    {
        public static MachineRetModel MapTo(MachineDto pDto)
        {
            return new()
            {
                Id = pDto.Id,
                NameKey = string.Empty,
                ItemResUrl = RetMapperUtils.GetUrl(pDto.Item),
                MoveResUrl = RetMapperUtils.GetUrl(pDto.Move),
                VersionGroupResUrl = RetMapperUtils.GetUrl(pDto.VersionGroup)
            };
        }
    }
}
