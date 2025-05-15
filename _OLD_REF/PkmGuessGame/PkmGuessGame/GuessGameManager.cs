using PkmGuessGame.Models;
using PkmGuessGame.Models.Inputs;
using PkmGuessGame.Models.Results;
using PkmGuessGame.Models.States;

namespace PkmGuessGame
{
    public class GuessGameManager
    {
        private readonly GameStateModel _state = new();

        #region NewGame
        public void NewGame(PkmInputsModel pInputs)
        {
            SetAbilities(pInputs.Abilities);
            SetMoves(pInputs.Moves);

            _state.OldMoves = [.. pInputs.OldMoves.Select(x => x.Name.ToUpper())];

            int potentialScore = (_state.Abilities.Count() + _state.Moves.Count()) * DefaultValues.StartingPoints;
            _state.Stats = new()
            {
                MaxPotentialScore = potentialScore,
                PotentialScore = potentialScore
            };
        }

        private void SetAbilities(IEnumerable<AbilityInputModel> pAbilities)
        {
            static Dictionary<HintTypes, HintStateModel> GetHints(AbilityInputModel pModel)
            {
                Dictionary<HintTypes, HintStateModel> hints = [];

                hints.Add(HintTypes.FlavorText, new()
                {
                    Hint = pModel.FlavorText,
                    ScoreCost = DefaultValues.FlavorHintCost,
                });

                return hints;
            }
            ;

            SetStateDict(_state.Abilities, pAbilities, GetHints);
        }

        private void SetMoves(IEnumerable<MoveInputModel> pMoves)
        {
            static Dictionary<HintTypes, HintStateModel> GetHints(MoveInputModel pModel)
            {
                Dictionary<HintTypes, HintStateModel> hints = [];

                hints.Add(HintTypes.DamageClass, new()
                {
                    Hint = pModel.DamageClass,
                    ScoreCost = DefaultValues.ClassHintCost,
                });

                hints.Add(HintTypes.Type, new()
                {
                    Hint = pModel.Type,
                    ScoreCost = DefaultValues.TypeHintCost,
                });

                hints.Add(HintTypes.FlavorText, new()
                {
                    Hint = pModel.FlavorText,
                    ScoreCost = DefaultValues.FlavorHintCost,
                });

                return hints;
            }
            ;

            SetStateDict(_state.Moves, pMoves, GetHints);
        }

        private static void SetStateDict<TInput>(StateDict pStateDict, IEnumerable<TInput> pInputs, Func<TInput, Dictionary<HintTypes, HintStateModel>> pGetHints) where TInput : BaseInputModel
        {
            static string? GetNameKey(TInput input)
            {
                return input.Name.ToUpper();
            }

            static int? GetId(TInput input)
            {
                return input.Id;
            }

            GuessStateModel? GetState(TInput input)
            {
                return new()
                {
                    CurrentScore = DefaultValues.StartingPoints,
                    Hints = pGetHints(input),
                    IsAnswered = false
                };
            }

            pStateDict.Clear();
            pStateDict.Add(pInputs, GetNameKey, GetId, GetState);
        }
        #endregion

        #region
        public GuessResultModel ProcessGuess(string pGuess)
        {
            string guessKey = pGuess.ToUpper();

            if (_state.Guesses.Contains(guessKey))
            {
                return new()
                {
                    Result = GuessResultTypes.AlreadyGuessed,
                    ScoreChange = 0,
                    GuessType = null,
                    GuessId = null,
                    IsGameDone = false
                };
            }

            _state.Stats.TotalNbrOfGuesses++;

            GuessResultModel result = ProcessAbilityGuess(guessKey);
            if (result.Result == GuessResultTypes.Wrong)
            {
                result = ProcessMoveGuess(guessKey);
            }

            _state.Stats.CurrentScore += result.ScoreChange;
            if (result.ScoreChange < 0)
            {
                _state.Stats.CurrentLoss += Math.Abs(result.ScoreChange);
                _state.Stats.PotentialScore += result.ScoreChange;
            }

            if (result.Result == GuessResultTypes.Wrong)
            {
                _state.Stats.NbrWrong++;
            }
            else if (result.Result == GuessResultTypes.Old)
            {
                _state.Stats.NbrOld++;
            }
            else
            {
                _state.Stats.NbrCorrect++;
            }

            return result;
        }

        private GuessResultModel ProcessAbilityGuess(string pGuess)
        {
            GuessResultTypes result;
            int ScoreChange;
            int? guessId;
            bool isGameDone;

            if (!_state.Abilities.Contains(pGuess))
            {
                result = GuessResultTypes.Wrong;
                ScoreChange = DefaultValues.WrongGuessPenalty;
                guessId = null;
                isGameDone = false;
            }
            else
            {
                int id = _state.Abilities[pGuess];
                result = GuessResultTypes.Correct;
                ScoreChange = _state.Abilities[id].CurrentScore;
                guessId = id;
                isGameDone = IsGameDone();

                _state.Abilities.Remove(pGuess);
            }

            return new()
            {
                Result = result,
                ScoreChange = ScoreChange,
                GuessType = GuessTypes.Ability,
                GuessId = guessId,
                IsGameDone = isGameDone
            };
        }

        private GuessResultModel ProcessMoveGuess(string pGuess)
        {
            GuessResultTypes result;
            int ScoreChange;
            int? guessId;
            bool isGameDone;

            if (!_state.Abilities.Contains(pGuess))
            {
                result = GuessResultTypes.Wrong;
                ScoreChange = DefaultValues.WrongGuessPenalty;
                guessId = null;
                isGameDone = false;
            }
            else if (_state.OldMoves.Contains(pGuess))
            {
                result = GuessResultTypes.Old;
                ScoreChange = 0;
                guessId = null;
                isGameDone = false;
            }
            else
            {
                int id = _state.Abilities[pGuess];
                result = GuessResultTypes.Correct;
                ScoreChange = _state.Abilities[id].CurrentScore;
                guessId = id;
                isGameDone = IsGameDone();

                _state.Moves.Remove(pGuess);
            }

            return new()
            {
                Result = result,
                ScoreChange = ScoreChange,
                GuessType = GuessTypes.Ability,
                GuessId = guessId,
                IsGameDone = isGameDone
            };
        }
        #endregion

        #region RevealHint
        public HintResultModel RevealHint(GuessTypes pGuessType, int pId, HintTypes pHintType)
        {
            HintResultModel? result;
            StateDict stateDict;

            HintResultModel nullResult = new()
            {
                Result = HintResultTypes.Invalid,
                ScoreCost = 0,
                Hint = null
            };

            switch (pGuessType)
            {
                case GuessTypes.Ability:
                    result = RevealAbilityHint(pId, pHintType);
                    stateDict = _state.Abilities;
                    break;
                case GuessTypes.Move:
                    result = RevealMoveHint(pId, pHintType);
                    stateDict = _state.Moves;
                    break;
                default:
                    return nullResult;
            }

            if (result is null)
            {
                return nullResult;
            }
            else
            {
                _state.Stats.PotentialScore -= result.ScoreCost;
                stateDict[pId].CurrentScore -= result.ScoreCost;
            }

            return result;
        }

        private HintResultModel? RevealAbilityHint(int pId, HintTypes pHintType)
        {
            if (pHintType != HintTypes.FlavorText)
            {
                return null;
            }

            if (!_state.Abilities.Contains(pId))
            {
                return null;
            }

            if (!_state.Abilities[pId].Hints.TryGetValue(pHintType, out HintStateModel? state))
            {
                return null;
            }

            return GetHint(_state.Abilities, pId, state);
        }

        private HintResultModel? RevealMoveHint(int pId, HintTypes pHintType)
        {
            if (!_state.Moves.Contains(pId))
            {
                return null;
            }

            if (!_state.Moves[pId].Hints.TryGetValue(pHintType, out HintStateModel? state))
            {
                return null;
            }

            return GetHint(_state.Moves, pId, state);
        }

        private static HintResultModel GetHint(StateDict pStateDict, int pId, HintStateModel pState)
        {
            HintResultTypes result;
            int scoreCost = 0;
            string? hint = null;

            if (pStateDict[pId].IsAnswered)
            {
                result = HintResultTypes.AlreadyGuessed;
            }
            else if (pState.IsRevealed)
            {
                result = HintResultTypes.AlreadyRevealed;
            }
            else
            {
                result = HintResultTypes.Ok;
                scoreCost = pState.ScoreCost;
                hint = pState.Hint;
            }

            return new()
            {
                Result = result,
                ScoreCost = scoreCost,
                Hint = hint
            };
        }
        #endregion

        #region
        public GameStats GetStats()
        {
            return _state.Stats;
        }
        #endregion

        private bool IsGameDone()
        {
            return _state.Abilities.Empty() && _state.Moves.Empty();
        }
    }
}
