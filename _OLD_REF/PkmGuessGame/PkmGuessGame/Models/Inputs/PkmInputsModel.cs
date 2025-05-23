namespace PkmGuessGame.Models.Inputs
{
    public class PkmInputsModel
    {
        public IEnumerable<AbilityInputModel> Abilities { get; set; } = [];
        public IEnumerable<MoveInputModel> Moves { get; set; } = [];
        public IEnumerable<OldMoveInputModel> OldMoves { get; set; } = [];
    }
}
