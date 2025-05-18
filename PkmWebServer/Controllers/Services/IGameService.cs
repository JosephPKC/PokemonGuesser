using PkmWebServer.Controllers.Results.Guess;
using PkmWebServer.Controllers.Results.Hint;
using PkmWebServer.Dtos.Hint;
using PkmWebServer.Models.Refs;
using PkmWebServer.Models.States;

namespace PkmWebServer.Controllers.Services
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
