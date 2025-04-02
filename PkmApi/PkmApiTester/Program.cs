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
            log.SetLogLevel(LogLevel.Info);
            PkmApiManager api = new("v2", pLogger: log);

            //  Get a single Pokemon
            string pkmId = "32";
            Console.WriteLine($"*** BEGIN: GET SINGLE POKEMON ({pkmId})");
            PkmDTO? singleRes = api.Pokemon.GetById(pkmId);
            if (singleRes != null)
            {
                Console.WriteLine(JsonSerializer.Serialize(singleRes));
            }
            Console.WriteLine($"*** END: GET SINGLE POKEMON ({pkmId})");

            //  Get the first 50 results
            int limit = 50;
            Console.WriteLine($"*** BEGIN: GET FIRST {limit} POKEMON");
            ResLiDTO? allRes = api.Pokemon.GetAll(limit, 0);
            if (allRes != null)
            {
                Console.WriteLine(JsonSerializer.Serialize(allRes));
            }
            Console.WriteLine($"*** END: GET FIRST {limit} POKEMON");
        }
    }
}