using PkmWebServer.Controllers;
using PkmWebServer.Controllers.Services;
using PkmWebServer.Services.ActiveGameRepo;
using PkmWebServer.Services.DataService;
using PkmWebServer.Services.GameService;
using PkmWebServer.Services.LogService;
using PkmWebServer.Services.UserService;
using PkmWebServer.TestData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Services
builder.Services.AddSingleton<IDataService>(new DataServiceAdapter(new TestDataRepo()));
builder.Services.AddSingleton<IGameService>(new GameServiceAdapter(ActiveGameRepoAdapter.Instance, new LogServiceAdapter<GameServiceAdapter>(LogLevel.Debug)));
builder.Services.AddSingleton<IUserService>(new UserServiceAdapter(ActiveGameRepoAdapter.Instance));

// Utils
builder.Services.AddSingleton<ILogService<ApiGatewayController>>(new LogServiceAdapter<ApiGatewayController>(LogLevel.Debug));

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
