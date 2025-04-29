using PkmApi.Dtos.Item.Item;

using PkmDataRetrieval.Retrieval.Models.Item;

namespace PkmDataRetrieval.Adapter.Mappers
{
    internal static class ItemRetMapper
    {
        public static ItemRetModel MapTo(ItemDto pDto)
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
