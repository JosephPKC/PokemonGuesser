using Swashbuckle.AspNetCore.SwaggerGen;

namespace PkmGuessGame
{
    public class GuessGameManager
    {
        protected readonly int _initScore = 10;
        protected readonly int _scorePenalty = -2;

        /* Guess Game State */
        protected bool _gameReady = false;
        protected bool _gameDone = false;

        protected readonly Dictionary<string, int> _moves = [];
        protected readonly Dictionary<int, MoveState> _moveStates = [];

        protected readonly Dictionary<string, int> _oldMoves = [];

        protected readonly HashSet<string> _guesses = [];
        protected readonly GameStats _stats = new();

        public void NewGame(PkmGameModel pPkmModel)
        {
            _moves.Clear();
            _moveStates.Clear();
            foreach (KeyValuePair<int, PkmMoveModel> movePair in pPkmModel.Moves)
            {
                string nameKey = movePair.Value.Name.ToUpper();

                if (_moves.ContainsKey(nameKey))
                {
                    //  Duplicate moves for some reason.
                    continue;
                }

                _moves.Add(nameKey, movePair.Key);

                MoveState moveSet = new()
                {
                    Id = movePair.Key,
                    Name = nameKey,
                    CurrentScore = _initScore,
                    Hints = GetMoveHints(movePair.Value),
                    IsAnswered = false
                };

                if (_moveStates.ContainsKey(movePair.Key))
                {
                    //  Duplicate moves for some reason.
                    continue;
                }

                _moveStates.Add(movePair.Key, moveSet);
            }

            _oldMoves.Clear();
            foreach (KeyValuePair<int, PkmOldMoveModel> movePair in pPkmModel.OldMoves)
            {
                string nameKey = movePair.Value.Name.ToUpper();

                if (_moves.ContainsKey(nameKey))
                {
                    //  Duplicate moves for some reason.
                    continue;
                }

                _oldMoves.Add(nameKey, movePair.Key);
            }

            _guesses.Clear();

            _stats.NbrOfGuessesTotal = 0;
            _stats.NbrOfCorrectGuesses = 0;
            _stats.NbrOfIncorrectGuesses = 0;
            _stats.NbrOfOldCorrectGuesses = 0;

            _gameReady = true;
            _gameDone = false;
        }

        protected Dictionary<MoveHintTypes, MoveHint> GetMoveHints(PkmMoveModel pMove)
        {
            Dictionary<MoveHintTypes, MoveHint> moveHints = [];

            moveHints.Add(MoveHintTypes.DamageClass, new()
            {
                Hint = pMove.MoveDamageClass,
                ScoreCost = 1,
                IsRevealed = false
            });

            moveHints.Add(MoveHintTypes.Type, new()
            {
                Hint = pMove.Type,
                ScoreCost = 2,
                IsRevealed = false
            });

            moveHints.Add(MoveHintTypes.FlavorText, new()
            {

                Hint = pMove.FlavorText,
                ScoreCost = 5,
                IsRevealed = false
            });

            return moveHints;
        }

        public ProcessGuessResult ProcessGuess(string pGuess)
        {
            if (!_gameReady)
            {
                throw new InvalidOperationException("Call NewGame() first to load up a new game.");
            }

            if (_gameDone)
            {
                throw new InvalidOperationException("Game is done. Reset the game with NewGame().");
            }

            string guessKey = pGuess.ToUpper();
            if (_guesses.Contains(guessKey))
            {
                return new()
                {
                    Result = GuessResults.Duplicate,
                    Score = 0,
                    GuessId = null,
                    IsGameDone = _gameDone,
                    CurrentTotalScore = _stats.TotalScore
                };
            }

            _guesses.Add(guessKey);
            _stats.NbrOfGuessesTotal++;

            GuessResults result;
            int score;
            int? guessId;

            if (_moves.TryGetValue(guessKey, out int moveId))
            {
                MoveState moveState = _moveStates[moveId];
                result = GuessResults.Correct;
                score = moveState.CurrentScore;
                guessId = moveState.Id;

                _stats.NbrOfCorrectGuesses++;
                _stats.TotalScore += score;

                _moves.Remove(guessKey);
                _moveStates[moveId].IsAnswered = true;

                if (_moves.Count == 0)
                {
                    _gameDone = true;
                }
            }
            else if (_oldMoves.TryGetValue(guessKey, out int oldMoveId))
            {
                result = GuessResults.OldMatch;
                score = 0;
                guessId = oldMoveId;

                _stats.NbrOfOldCorrectGuesses++;

                _oldMoves.Remove(guessKey);
            }
            else
            {
                result = GuessResults.Incorrect;
                score = _scorePenalty;
                guessId = null;

                _stats.NbrOfIncorrectGuesses++;
                _stats.TotalScore += score;
            }

            return new()
            {
                Result = result,
                Score = score,
                GuessId = guessId,
                IsGameDone = _gameDone,
                CurrentTotalScore = _stats.TotalScore
            };
        }

        public RevealHintResult RevealHint(int pMoveId, MoveHintTypes pHintType)
        {
            if (!_gameReady)
            {
                throw new InvalidOperationException("Call NewGame() first to load up a new game.");
            }

            if (_gameDone)
            {
                throw new InvalidOperationException("Game is done. Reset the game with NewGame().");
            }

            bool dictRes = _moveStates.TryGetValue(pMoveId, out MoveState? moveState);
            if (!dictRes || moveState is null)
            {
                throw new ArgumentException($"{pMoveId} is not a part of the guess set.");
            }

            HintResults result;
            string? hint;

            bool hintDictRes = moveState.Hints.TryGetValue(pHintType, out MoveHint? moveHint);
            if (moveState.IsAnswered)
            {
                result = HintResults.AlreadyAnswered;
                hint = null;
            }
            else if (!hintDictRes || moveHint is null)
            {
                result = HintResults.Missing;
                hint = null;
            }
            else if (moveHint.IsRevealed)
            {
                result = HintResults.AlreadyRevealed; 
                hint = null;
            }
            else
            {
                result = HintResults.Revealed;
                hint = moveHint.Hint;

                _moveStates[pMoveId].CurrentScore -= moveHint.ScoreCost;
                _moveStates[pMoveId].Hints[pHintType].IsRevealed = true;
            }

            return new()
            {
                Result = result,
                Hint = hint
            };
        }

        public GameStats GetStats()
        {
            return _stats;
        }
    }
}
