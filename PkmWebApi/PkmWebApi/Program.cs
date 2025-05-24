using PkmWebApi.Controllers;
using PkmWebApi.Controllers.Services;
using PkmWebApi.Services.ActiveGameRepo;
using PkmWebApi.Services.DataService;
using PkmWebApi.Services.GameService;
using PkmWebApi.Services.LogService;
using PkmWebApi.Services.UserService;
using PkmWebApi.TestData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(x => x.AddPolicy("CorsPolicy", builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));

/* Services */
builder.Services.AddSingleton<IDataService>(new DataServiceAdapter(new TestDataRepo()));
builder.Services.AddSingleton<IGameService>(new GameServiceAdapter(ActiveGameRepoAdapter.Instance, new LogServiceAdapter<GameServiceAdapter>(LogLevel.Debug)));
builder.Services.AddSingleton<IUserService>(new UserServiceAdapter(ActiveGameRepoAdapter.Instance));

builder.Services.AddSingleton<ILogService<ApiGatewayController>>(new LogServiceAdapter<ApiGatewayController>(LogLevel.Debug));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
