using Server.Models;
using System.Collections.Concurrent;

namespace Server.Services
{
    public class GameStateCacheServiceAdapter
    {
        public ConcurrentDictionary<string, GameStateModel> ActiveGames = [];

        public void SpillDict()
        {
            Console.WriteLine("Spilling dict: ");
            foreach (var pkm in ActiveGames)
            {
                Console.WriteLine($"{pkm.Key}: {pkm.Value.PkmRef.Name}");
            }
        }

        public GameStateModel AddOrUpdate(string pKey, GameStateModel pValue)
        {
            Console.WriteLine($"Adding or updating: {pKey} => {pValue.PkmRef.Name}");
            return pValue;
        }
    }
}
