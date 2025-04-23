using StackExchange.Redis;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Api;
using PkmDataRetrieval.Retrieval;

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
            pBuilder.Services.AddSingleton<IDataRetrieval>(DataRetrievalFactory.CreateDataRetriever(PkmGatewayFactory.CreateGateway(), ConnectionMultiplexer.Connect(Config.RedisKubeConnect, config => config.AbortOnConnectFail = false), Config.CurrentGenId));
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
