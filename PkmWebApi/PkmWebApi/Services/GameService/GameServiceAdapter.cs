using PkmWebApi.Dtos.Game;
using PkmWebApi.Dtos.Hint;
using PkmWebApi.Dtos.Results.Guess;
using PkmWebApi.Dtos.Results.Hint;
using PkmWebApi.Controllers.Services;
using PkmWebApi.Models.Refs;
using PkmWebApi.Models.States;
using PkmWebApi.Utils;
using PkmWebApi.Utils.ServiceOperationException;

namespace PkmWebApi.Services.GameService;
public class GameServiceAdapter(IActiveGameRepo pRepo, ILogService<GameServiceAdapter> pLog) : IGameService
{
    private readonly IActiveGameRepo _activeGames = pRepo;
    private readonly ILogService<GameServiceAdapter> _log = pLog;

    #region IGameStateService
    public GameStateModel CreateNewGame(string pUserId, PkmRefModel pPkmRef)
    {
        GameStateModel newState = GameStateMapper.CreateState(pPkmRef);
        _activeGames.AddOrUpdate(pUserId, newState, (key, state) => newState);
        return newState;
    }

    public GameStateModel GetActiveGame(string pUserId)
    {
        if (!_activeGames.TryGetValue(pUserId, out GameStateModel? state))
        {
            throw new ServiceOperationException($"{pUserId} has no active games.", ExceptionFaultTypes.ArgumentNotFound);
        }

        return state!;
    }

    public StatsModel GetStats(string pUserId)
    {
        GameStateModel state = GetActiveGame(pUserId);
        return state.Stats;
    }

    public GuessResultTypes ProcessGuess(string pUserId, string pGuess)
    {
        string guess = NameCleaner.CleanNameKey(pGuess);
        _log.Debug($"Processing Guess {guess}");
        GameStateModel state = GetActiveGame(pUserId);

        if (state.Guesses.Contains(pGuess))
        {
            return GuessResultTypes.AlreadyGuessed;
        }

        state.Stats.NbrGuesses++;
        state.Guesses.Add(pGuess);
        state.Result = GameResultTypes.Ongoing;

        if (state.MoveNameKeys.TryGetValue(guess, out int moveId))
        {
            _log.Debug($"Guess {guess} was CORRECT.");
            MoveStateModel moveState = state.MoveStates[moveId];
            state.Stats.NbrCorrect++;
            state.Stats.CurrentScore += moveState.Points;

            SetMoveAsAnswered(moveState);

            if (IsGameWon(state))
            {
                state.Result = GameResultTypes.Win;
            }

            return GuessResultTypes.Correct;
        }

        _log.Debug($"Guess {guess} was INCORRECT.");

        state.WrongGuesses.Add(pGuess);
        state.Lives--;

        if (IsGameLost(state))
        {
            state.Result = GameResultTypes.Lose;

            SetAllMovesAsAnswered(state);
        }

        return GuessResultTypes.Incorrect;
    }

    public HintResultTypes RevealHint(string pUserId, int pMoveId, HintTypes pHintType)
    {
        GameStateModel state = GetActiveGame(pUserId);

        if (!state.MoveStates.TryGetValue(pMoveId, out MoveStateModel? move))
        {
            throw new ServiceOperationException($"Move {pMoveId} does not exist for the game.", ExceptionFaultTypes.ArgumentNotFound);
        }

        if (move.IsAnswered)
        {
            return HintResultTypes.AlreadyAnswered;
        }

        HintStateModel? hint = GetHintStateByType(move, pHintType) ?? 
            throw new ServiceOperationException($"Invalid hint type ${pHintType} for move {pMoveId}.", ExceptionFaultTypes.ArgumentInvalid);

        if (hint.IsRevealed)
        {
            return HintResultTypes.AlreadyRevealed;
        }

        hint.IsRevealed = true;
        state.Stats.PotentialScore -= hint.Ref.ScoreCost;
        move.Points -= hint.Ref.ScoreCost;

        return HintResultTypes.Revealed;
    }

    public void UpdateGameState(string pUserId, GameStateModel pGameState)
    {
        if (!_activeGames.ContainsKey(pUserId))
        {
            throw new ServiceOperationException($"{pUserId} has no active games.", ExceptionFaultTypes.ArgumentNotFound);
        }

        _activeGames.AddOrUpdate(pUserId, pGameState, (key, state) => pGameState);
    }
    #endregion

    private static void SetAllMovesAsAnswered(GameStateModel pGameState)
    {
        foreach (MoveStateModel move in pGameState.MoveStates.Values)
        {
            SetMoveAsAnswered(move);
        }
    }

    private static void SetMoveAsAnswered(MoveStateModel pMoveState)
    {
        pMoveState.IsAnswered = true;
        pMoveState.DamageClass.IsRevealed = true;
        pMoveState.FlavorText.IsRevealed = true;
        pMoveState.Stats.IsRevealed = true;
        pMoveState.Type.IsRevealed = true;
    }

    private static bool IsGameWon(GameStateModel pGameState)
    {
        return pGameState.MoveStates.Values.All(state => state.IsAnswered);
    }

    private static bool IsGameLost(GameStateModel pGameState)
    {
        return pGameState.Lives == 0;
    }

    private static HintStateModel? GetHintStateByType(MoveStateModel pMove, HintTypes pHintType)
    {
        return pHintType switch
        {
            HintTypes.DamageClass => pMove.DamageClass,
            HintTypes.FlavorText => pMove.FlavorText,
            HintTypes.Stats => pMove.Stats,
            HintTypes.Type => pMove.Type,
            _  => null
        };
    }
}
