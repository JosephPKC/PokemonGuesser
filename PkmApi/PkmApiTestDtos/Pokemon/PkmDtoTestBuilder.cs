using PkmApi.Dtos.Pokemon.Pokemon;

namespace PkmApiTestDtos.Pokemon
{
    internal class PkmDtoTestBuilder : IDtoTestBuilder<PkmDto>
    {
        #region IDtoTestBuilder<PkmDto>
        public PkmDto GetBasic()
        {
            return new(1, "pkm");
        }

        public PkmDto GetEmpty()
        {
            return new(
                1, "pkm", 1, 1, true, 1, 1, [], [], [], [], "location-area-encounters",
                [], [], new(), new(), new("species", "species/1"), [], []
            );
        }

        public PkmDto GetFull()
        {
            return new(
                1, "pkm", 1, 1, true, 1, 1, 
                [new(true, 1, new("ability", "abilities/1"))], 
                [new("form", "forms/1")], 
                [new(1, new("version", "versions/1"))], 
                [new(
                    new("item", "items/1"), 
                    [new(new("version", "versions/1"), 1)]
                )], 
                "location-area-encounters",
                [new(
                    new("move", "moves/1"),
                    [new(
                        new("move-learn-method", "move-learn-methods/1"),
                        new("version-group", "version-groups/1"),
                        1
                    )]
                )],
                [new(
                    new("gen", "gens/1"),
                    [new(1, new("type", "types/1"))]
                )],
                new(
                    "front-default", "front-shiny", "front-female", "front-shiny-female",
                    "back-default", "back-shiny", "back-female", "back-shiny-female"
                ), 
                new("latest", "legacy"), 
                new("species", "species/1"), 
                [new(new("stat", "stats/1"), 1, 1)], 
                [new(1, new("type", "types/1"))]
            );
        }

        public PkmDto GetShallow()
        {
            return new(
                1, "pkm", 1, 1, true, 1, 1,
                [new()],
                [new("form", "forms/1")],
                [new()],
                [new()],
                "location-area-encounters",
                [new()],
                [new()],
                new(),
                new("latest", "legacy"),
                new("species", "species/1"),
                [new()],
                [new()]
            );
        }
        #endregion
    }
}
