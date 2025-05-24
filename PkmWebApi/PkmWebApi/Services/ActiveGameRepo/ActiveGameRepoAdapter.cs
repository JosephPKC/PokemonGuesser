using System.Collections.Concurrent;
using PkmWebApi.Models.States;

namespace PkmWebApi.Services.ActiveGameRepo
{
    public class ActiveGameRepoAdapter : IActiveGameRepo
    {
        private readonly ConcurrentDictionary<string, GameStateModel> _activeGames = [];

        public static ActiveGameRepoAdapter Instance { get; } = new();

        private ActiveGameRepoAdapter() { }
        static ActiveGameRepoAdapter() { }

        #region IActiveGameRepo
        public void AddOrUpdate(string pUserId, GameStateModel pState, Func<string, GameStateModel, GameStateModel> pUpdateState)
        {
            _ = _activeGames.AddOrUpdate(pUserId, pState, pUpdateState);
        }

        public bool ContainsKey(string pUserId)
        {
            return _activeGames.ContainsKey(pUserId);
        }

        public GameStateModel GetState(string pUserId)
        {
            return _activeGames[pUserId];
        }

        public bool TryGetValue(string pUserId, out GameStateModel? pState)
        {
            return _activeGames.TryGetValue(pUserId, out pState);
        }
        #endregion
    }
}
