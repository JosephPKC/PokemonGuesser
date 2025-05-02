using StackExchange.Redis;

using LogWrapper;
using RedisCache;
using RedisCache.Redis;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Api;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval
{
    internal static class ServiceBuilder
    {
        public static WebApplication BuildServiceApp(string[] pArgs)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(pArgs);
            ConfigureServices(builder);

            WebApplication app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            ConfigureForDevEnv(app);

            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder pBuilder)
        {
            //  Core Services
            pBuilder.Services.AddControllers();
            pBuilder.Services.AddEndpointsApiExplorer();
            pBuilder.Services.AddSwaggerGen();
            pBuilder.Services.AddOpenApi();

            //  DI
            pBuilder.Services.AddTransient(x => LoggerFacFactory.CreateColorConsoleLoggerFactory());
            ConfigureRetrieval(pBuilder);
        }

        private static void ConfigureRetrieval(WebApplicationBuilder pBuilder)
        {
            IPkmGateway pkmGateway = PkmGatewayFactory.CreateGateway();
            IConnectionMultiplexer connMulti = ConnectionMultiplexer.Connect(Config.RedisKubeConnect, config => config.AbortOnConnectFail = false);
            IRedisHandler redisHandler = RedisHandlerFactory.CreateNewRedisHandler(connMulti, Config.ServiceKeyPrefix);
            ICacheHandler cacheHandler = CacheHandlerFactory.CreateNewCacheHandler(redisHandler);
            LogWrapper.Loggers.ILoggerFactory loggerFactory = LoggerFacFactory.CreateColorConsoleLoggerFactory();
            IDataRetrieval dataRetriever = DataRetrievalFactory.CreateDataRetriever(pkmGateway, cacheHandler, loggerFactory, Config.CurrentGenId);

            pBuilder.Services.AddSingleton(dataRetriever);
        }

        private static void ConfigureForDevEnv(WebApplication pApp)
        {
            if (!pApp.Environment.IsDevelopment())
            {
                return;
            }

            pApp.MapOpenApi();
            pApp.UseSwagger();
            pApp.UseSwaggerUI();
        }
    }
}
