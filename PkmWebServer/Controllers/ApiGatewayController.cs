using Microsoft.AspNetCore.Mvc;
using PkmWebServer.Controllers.Inputs;
using PkmWebServer.Controllers.Results.Game;
using PkmWebServer.Controllers.Results.Guess;
using PkmWebServer.Controllers.Results.Hint;
using PkmWebServer.Controllers.Results.Stats;
using PkmWebServer.Controllers.Results.User;
using PkmWebServer.Controllers.Services;
using PkmWebServer.Models.Refs;
using PkmWebServer.Models.States;
using PkmWebServer.Utils.ServiceOperationException;

namespace PkmWebServer.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class ApiGatewayController : ControllerBase
    {
        private readonly IDataService _data;
        private readonly IGameService _game;
        private readonly ILogService<ApiGatewayController> _log;
        private readonly IUserService _user;

        public ApiGatewayController(IDataService pData, IGameService pGame, ILogService<ApiGatewayController> pLog, IUserService pUser)
        {
            _data = pData;
            _game = pGame;
            _log = pLog;
            _user = pUser;
        }

        [HttpGet("game")]
        [ProducesResponseType<GetActiveGameResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetActiveGame([FromHeader] string userId)
        {
            _log.Info($"GET /game (GetActiveGame): {userId}");

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest($"UserId {userId} is blank.");
            }

            try
            {
                GameStateModel state = _game.GetActiveGame(userId);
                GetActiveGameResultDto result = GetActiveGameResultMapper.CreateResult(state);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("game")]
        [ProducesResponseType<CreateNewGameResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNewGame([FromHeader] string userId)
        {
            _log.Info($"POST /game (CreateNewGame): {userId}");

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest($"UserId {userId} is blank.");
            }

            try
            {
                PkmRefModel pkm = _data.GetRandomPkm();
                GameStateModel state = _game.CreateNewGame(userId, pkm);
                CreateNewGameResultDto result = CreateNewGameResultMapper.CreateResult(state);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("guess")]
        [ProducesResponseType<ProcessGuessResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ProcessGuess([FromHeader] string userId, [FromBody] GuessInputDto guess)
        {
            _log.Info($"PUT /guess (ProcessGuess): {userId} / {guess.Guess}");

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest($"UserId {userId} is blank.");
            }

            if (string.IsNullOrWhiteSpace(guess.Guess))
            {
                return BadRequest($"Guess {guess.Guess} is blank.");
            }

            try
            {
                GuessResultTypes guessResult = _game.ProcessGuess(userId, guess.Guess);
                GameStateModel state = _game.GetActiveGame(userId);
                ProcessGuessResultDto result = ProcessGuessResultMapper.GetResult(state, guessResult);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("hint")]
        [ProducesResponseType<RevealHintResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RevealHint([FromHeader] string userId, [FromBody] HintInputDto hintToReveal)
        {
            _log.Info($"PUT /hint (RevealHint): {userId} / {hintToReveal.MoveId} / {hintToReveal.HintType}");

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest($"UserId {userId} is blank.");
            }

            try
            {
                HintResultTypes hintResult = _game.RevealHint(userId, hintToReveal.MoveId, hintToReveal.HintType);
                GameStateModel state = _game.GetActiveGame(userId);
                RevealHintResultDto result = RevealHintResultMapper.GetResult(state, hintResult);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("stats")]
        [ProducesResponseType<GetStatsResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetStats([FromHeader] string userId)
        {
            _log.Info($"GET /stats (GetStats): {userId}");

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest($"UserId {userId} is blank.");
            }

            try
            {
                StatsModel stats = _game.GetStats(userId);
                GetStatsResultDto result = GetStatsResultMapper.GetResult(stats);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("user")]
        [ProducesResponseType<ValidateUserResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ValidateUser([FromHeader] string userId)
        {
            _log.Info($"GET /user (ValidateUser)");

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest($"UserId {userId} is blank.");
            }

            try
            {
                bool userExists = _user.ValidateUser(userId);
                ValidateUserResultDto result = ValidateUserResultMapper.CreateResult(userExists);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("user")]
        [ProducesResponseType<CreateUserResultDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser()
        {
            _log.Info($"POST /user (CreateUser)");

            try
            {
                string userId = _user.CreateNewUser();
                CreateUserResultDto result = CreateUserResultMapper.CreateResult(userId);
                return Ok(result);
            }
            catch (ServiceOperationException ex)
            {
                return GetStatusCodeResult(ex);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private ObjectResult GetStatusCodeResult(ServiceOperationException pEx)
        {
            _log.Debug($"Got SOE {pEx.FaultType}: {pEx.Message}.");
            return pEx.FaultType switch { 
                ExceptionFaultTypes.ArgumentNotFound => NotFound(pEx.Message),
                ExceptionFaultTypes.ArgumentInvalid => BadRequest(pEx.Message),
                ExceptionFaultTypes.Misc => StatusCode(StatusCodes.Status500InternalServerError, pEx.Message),
                _ => StatusCode(StatusCodes.Status500InternalServerError, pEx.Message)
            };
        }
    }
}
