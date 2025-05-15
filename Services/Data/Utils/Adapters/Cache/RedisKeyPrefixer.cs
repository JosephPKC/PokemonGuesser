using Data.Models.Form;
using Data.Models.Generation;
using Data.Models.Move;
using Data.Models.MoveDamageClass;
using Data.Models.Pokedex;
using Data.Models.Pokemon;
using Data.Models.Species;
using Data.Models.Type;
using Data.Models.VersionGroup;

namespace Data.Utils.Adapters.Cache;
internal class RedisKeyPrefixer(string pServiceKeyPrefix)
{
    private readonly string _serviceKeyPrefix = pServiceKeyPrefix;
    public string GetKey<TModel>(string pKey)
    {
        return $"{_serviceKeyPrefix}:{RedisConfigs.DataModelKeyPrefix}:{GetDataModelPrefix<TModel>()}:{pKey}";
    }

    private static string GetDataModelPrefix<TModel>()
    {
        return typeof(TModel).Name switch
        {
            nameof(FormDataModel) => "form",
            nameof(GenerationDataModel) => "generation",
            nameof(MoveDataModel) => "move",
            nameof(MoveDamageClassDataModel) => "move-damage-class",
            nameof(PokedexDataModel) => "pokedex",
            nameof(PkmDataModel) => "pokemon",
            nameof(SpeciesDataModel) => "species",
            nameof(TypeDataModel) => "type",
            nameof(VersionGroupDataModel) => "version-group",
            _ => string.Empty
        };
    }
}
