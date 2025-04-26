using PkmApi.Dtos.Item.Item;

namespace PkmApiTestDtos.Item
{
    public class ItemDtoTestBuilder : IDtoTestBuilder<ItemDto>
    {
        #region IDtoTestBuilder<ItemDto>
        public ItemDto GetBasic()
        {
            return new(1, "item");
        }

        public ItemDto GetEmpty()
        {
            return new(
                1, "item", 1, 1, new("fling-effect", "fling-effects/1"), [], new("category", "categories/1"),
                [], [], [], [], new(), [], new("evolves/1"), []
            );
        }

        public ItemDto GetFull()
        {
            return new(
                1, "item", 1, 1, 
                new("fling-effect", "fling-effects/1"), 
                [new("attribute", "attributes/1")], 
                new("category", "categories/1"),
                [new("effect", "short-effect", new("language", "languages/1"))], 
                [new(new("language", "languages/1"), new("version-group", "version-groups/1"))], 
                [new(1, new("gen", "gens/1"))], 
                [new("name", new("language", "languages/1"))], 
                new("sprite-default"), 
                [new(
                    new("pkm", "pkm/1"),
                    [new(1, new("version", "versions/1"))]
                )], 
                new("evolves/1"), 
                [new(new("machines/1"), new("version-group", "version-groups/1"))]
            );
        }

        public ItemDto GetShallow()
        {
            return new(
                1, "item", 1, 1,
                new("fling-effect", "fling-effects/1"),
                [new("attribute", "attributes/1")],
                new("category", "categories/1"),
                [new()],
                [new()],
                [new()],
                [new()],
                new(),
                [new()],
                new("evolves/1"),
                [new()]
            );
        }
        #endregion
    }
}
