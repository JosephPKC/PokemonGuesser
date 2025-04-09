using System.Text.Json;

using PkmApi;
using PkmApi.Dtos;
using PkmApi.Dtos.Shared;
using PkmApi.Endpoints;
using PkmApi.Utils;

namespace PkmApiTester
{
    public static class Program
    {
        private static bool ShowJsonLogs { get; set; } = true;

        public static void Main(string[] pArgs)
        {
            //  Create the Api
            ILogger log = LoggerFactory.BuildConsoleLogger();
            log.SetLogLevel(LogLevel.Info);

            IPkmApi api = PkmApiFactory.CreatePkmApi("v2", pLogger: log, pCacheFactory: new CacheFactory());

            TestApi(api.Ability, log, "ABILITY", pShowJsonLogs: ShowJsonLogs);
            TestApi(api.Move, log, "MOVE", pShowJsonLogs: ShowJsonLogs);
            TestApi(api.Pokemon, log, "POKEMON", pShowJsonLogs: ShowJsonLogs);
            TestApi(api.Type, log, "TYPE", pShowJsonLogs: ShowJsonLogs);
            TestApi(api.Version, log, "VERSION", pShowJsonLogs: ShowJsonLogs);
            TestApi(api.VersionGroup, log, "VERSION GROUP", pShowJsonLogs: ShowJsonLogs);
        }

        private static void TestApi<TDto>(IEndpointHandler<TDto> pEndpoint, ILogger pLog, string pTestName, string pTestId = "1", int pTestLimit = 50, bool pShowJsonLogs = false) where TDto : IPkmApiDto
        {
            //  Get single
            string testSingleLine = $"GET SINGLE {pTestName} ({pTestId})";
            TDto? testSingle()
            {
                if (string.IsNullOrWhiteSpace(pTestId))
                {
                    return default;
                }

                return pEndpoint.GetById(pTestId);
            }

            TestApi(pLog, testSingleLine, testSingle, pShowJsonLogs);

            //  Get again
            TestApi(pLog, testSingleLine, testSingle, pShowJsonLogs);


            //  Get the first 50 results
            string testFirstNLine = $"GET FIRST {pTestLimit} {pTestName}";
            ResLiDto? testFirstN()
            {
                return pEndpoint.GetAll(pTestLimit);
            }

            TestApi(pLog, testFirstNLine, testFirstN, pShowJsonLogs);
        }

        private static void TestApi<TDto>(ILogger pLog, string pTestLine, Func<TDto?> pGetFromApi, bool pShowJsonLogs = false) where TDto : IPkmApiDto
        {
            pLog.Info($"*** BEGIN: {pTestLine}\n");
            TDto? res = pGetFromApi();
            pLog.Info(res != null ? "SUCCESS" : "FAILURE");
            if (res != null && pShowJsonLogs)
            {
                string serStr = JsonSerializer.Serialize(res);
                pLog.Info("*****\n");
                pLog.Info(serStr + "\n");
                pLog.Info("*****\n");
            }
            pLog.Info($"*** END: {pTestLine}\n");
        }
    }
}