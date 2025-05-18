using PkmWebServer.Models.States;

namespace PkmWebServer.Services;
public interface IActiveGameRepo
{
    void AddOrUpdate(string pUserId, GameStateModel pState, Func<string, GameStateModel, GameStateModel> pUpdateState);
    bool ContainsKey(string pUserId);
    GameStateModel GetState(string pUserId);
    bool TryGetValue(string pUserId, out GameStateModel? pState);
}
