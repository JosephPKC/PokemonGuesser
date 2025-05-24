using PkmWebApi.Dtos.Hint;
using PkmWebApi.Dtos.Results.Guess;
using PkmWebApi.Dtos.Results.Hint;
using PkmWebApi.Models.Refs;
using PkmWebApi.Models.States;

namespace PkmWebApi.Controllers.Services
{
    public interface IGameService
    {
        GameStateModel CreateNewGame(string pUserId, PkmRefModel pPkmRef);
        GameStateModel GetActiveGame(string pUserId);
        StatsModel GetStats(string pUserId);
        GuessResultTypes ProcessGuess(string pUserId, string pGuess);
        HintResultTypes RevealHint(string pUserId, int pMoveId, HintTypes pHintType);
        void UpdateGameState(string pUserId, GameStateModel pGameState);
    }
}
