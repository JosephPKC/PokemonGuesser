using StackExchange.Redis;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Api;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Utils.Cache;
using PkmDataRetrieval.Utils.Cache.Redis;

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
            ConfigureRedis(pBuilder);
        }

        private static void ConfigureRedis(WebApplicationBuilder pBuilder)
        {
            IPkmGateway pkmGateway = PkmGatewayFactory.CreateGateway();
            IConnectionMultiplexer connMulti = ConnectionMultiplexer.Connect(Config.RedisKubeConnect, config => config.AbortOnConnectFail = false);
            ICacheHandler cacheHandler = RedisHandlerFactory.CreateNewRedisHandler(connMulti, Config.ServiceKeyPrefix);
            IDataRetrieval dataRetriever = DataRetrievalFactory.CreateDataRetriever(pkmGateway, cacheHandler, Config.CurrentGenId);

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
