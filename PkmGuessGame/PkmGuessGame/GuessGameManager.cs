using PkmGuessGame.Guesses;
using PkmGuessGame.Hints;
using PkmGuessGame.Inputs;
using PkmGuessGame.Stats;

namespace PkmGuessGame
{
    public class GuessGameManager
    {
        private readonly StateDict _abilities = new();
        private readonly StateDict _moves = new();

        public void NewGame(IEnumerable<AbilityInputModel> pAbilities, IEnumerable<MoveInputModel> pMoves)
        {

        }

        public GuessResult ProcessGuess(string pGuess)
        {
            return new();
        }

        public HintResult RevealHints(int pId, HintTypes pHintType)
        {
            return new();
        }

        public GameStats GetStats()
        {
            return new();
        }
    }
}
