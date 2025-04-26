using PkmApi.Dtos.Pokemon.Form;

namespace PkmApiTestDtos.Pokemon
{
    internal class FormDtoTestBuilder : IDtoTestBuilder<FormDto>
    {
        #region IDtoTestBuilder<FormDto>
        public FormDto GetBasic()
        {
            return new(1, "form");
        }

        public FormDto GetEmpty()
        {
            return new(1, "form", 1, 1, true, true, true, "form-name", new("pkm", "pkms/1"), [], new(), new("version-group", "version-groups/1"), [], []);
        }

        public FormDto GetFull()
        {
            return new(
                1, "form", 1, 1, true, true, true, "form-name",
                new("pkm", "pkms/1"), 
                [new(1, new("type", "types/1"))], 
                new("front-default", "front-shiny", "back-default", "back-shiny"), 
                new("version-group", "version-groups/1"), 
                [new("name", new("language", "languages/1"))], 
                [new("form-name", new("language", "languages/1"))]
            );
        }

        public FormDto GetShallow()
        {
            return new(
                1, "form", 1, 1, true, true, true, "form-name",
                new("pkm", "pkms/1"),
                [new()],
                new(),
                new("version-group", "version-groups/1"),
                [new()],
                [new()]
            );
        }
        #endregion
    }
}
