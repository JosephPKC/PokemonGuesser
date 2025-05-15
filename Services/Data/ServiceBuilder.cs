using StackExchange.Redis;

using LogWrapper;
using Data.Utils;
using Data.Utils.Adapters.Cache;
using Data.Utils.Adapters.Log;
using Data.PkmApi.PkmApiAdapter;
using Data.PkmApi;


namespace Data;
internal static class ServiceBuilder
{
    public static WebApplication BuildServiceApp(string[] pArgs)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(pArgs);
        ConfigureServices(builder);

        WebApplication app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        ConfigureForDevEnv(app);

        return app;
    }

    private static void ConfigureServices(WebApplicationBuilder pBuilder)
    {
        pBuilder.Services.AddTransient(x => LoggerFacFactory.CreateColorConsoleLoggerFactory());
        ConfigureRetrieval(pBuilder);
    }

    private static void ConfigureRetrieval(WebApplicationBuilder pBuilder)
    {
        IPkmApiGateway pkmGateway = PkmApiGatewayFactory.CreateGateway();
        ICacheHandlerFactory cacheHandlerFactory = new RedisCacheHandlerFactory();
        ILogFactory logFactory = new LogFactory();
        IDataManager dataRetriever = DataManagerFactory.CreateDataManager(pkmGateway, cacheHandlerFactory, logFactory, Config.CurrentGenId);

        pBuilder.Services.AddSingleton(dataRetriever);
    }

    private static void ConfigureForDevEnv(WebApplication pApp)
    {
        if (!pApp.Environment.IsDevelopment())
        {
            return;
        }

    }
}
