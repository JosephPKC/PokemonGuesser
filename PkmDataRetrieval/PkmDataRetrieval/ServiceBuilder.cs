using StackExchange.Redis;

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
            pBuilder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Config.RedisConnect));
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
