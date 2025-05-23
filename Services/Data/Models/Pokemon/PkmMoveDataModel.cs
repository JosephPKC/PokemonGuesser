namespace Data.Models.Pokemon;
public class PkmMoveDataModel : BaseResDataModel
{
    public IEnumerable<PkmMoveVersDataModel> MoveVersions { get; set; } = [];
}
