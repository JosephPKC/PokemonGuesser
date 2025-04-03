using System.Text.Json;

using PkmApi;
using PkmApi.DTOs.Pokemon;
using PkmApi.DTOs.Shared;
using PkmApi.Utils;

namespace PkmApiTester
{
    public static class Program
    {
        public static void Main(string[] pArgs)
        {
            //  Create the Api
            ILogger log = LoggerFactory.BuildConsoleLogger();
            log.SetLogLevel(LogLevel.Debug);

            PkmApiManager api = new("v2", pLogger: log, pCacheFactory: new CacheFactory());

            //  Get a single Pokemon
            string pkmId = "32";
            Console.WriteLine($"*** BEGIN: GET SINGLE POKEMON ({pkmId})\n");
            PkmDTO? singleRes = api.Pokemon.GetById(pkmId);
            if (singleRes != null)
            {
                Console.WriteLine(JsonSerializer.Serialize(singleRes));
            }
            Console.WriteLine($"*** END: GET SINGLE POKEMON ({pkmId})\n");

            //  Get the same Pokemon again
            Console.WriteLine($"*** BEGIN: GET SAME POKEMON ({pkmId})\n");
            singleRes = api.Pokemon.GetById(pkmId);
            if (singleRes != null)
            {
                Console.WriteLine(JsonSerializer.Serialize(singleRes));
            }
            Console.WriteLine($"*** END: GET SAME POKEMON ({pkmId})\n");

            //  Get the first 50 results
            int limit = 50;
            Console.WriteLine($"*** BEGIN: GET FIRST {limit} POKEMON\n");
            ResLiDTO? allRes = api.Pokemon.GetAll(limit, 0);
            if (allRes != null)
            {
                Console.WriteLine(JsonSerializer.Serialize(allRes));
            }
            Console.WriteLine($"*** END: GET FIRST {limit} POKEMON\n");
        }
    }
}