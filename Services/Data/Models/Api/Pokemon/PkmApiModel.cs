namespace Data.Models.Api.Pokemon;
public class PkmApiModel : BasicApiModel
{
    public string SpriteUrl { get; set; } = string.Empty;
    public IEnumerable<NameApiModel> Types { get; set; } = [];
    public IEnumerable<PkmMoveApiModel> Moves { get; set; } = [];
}
