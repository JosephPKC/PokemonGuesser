using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server
{
    [ApiController]
    [Route("/api/")]
    public class ApiGatewayController : ControllerBase
    {
        DataServiceAdapter _data;
        GameStateCacheServiceAdapter _state;

        public ApiGatewayController(DataServiceAdapter pData, GameStateCacheServiceAdapter pState)
        {
            _data = pData;
            _state = pState;
        }

        //[HttpGet("pkm/{id}")]
        //public IActionResult GetPkm(int id, [FromHeader] string userId)
        //{
        //    Console.WriteLine($"Get pkm: {userId}.");
        //    _state.SpillDict();
        //    if (_state.ActiveGames.ContainsKey(userId))
        //    {
        //        Console.WriteLine($"Found pkm game for {userId}.");
        //        return Ok(_state.ActiveGames[userId].PkmRef);
        //    }

        //    PkmModel pkm = _data.GetRandomPkm();
        //    GameStateModel state = new()
        //    {
        //        PkmRef = pkm,
        //        IsDone = false,
        //        Moves = (IDictionary<int, MoveStateModel>)pkm.Moves.ToDictionary(x => x.Id)
        //    };
        //    _state.ActiveGames.AddOrUpdate(userId, state, _state.AddOrUpdate);
        //    return Ok(pkm);
        //}

        [HttpGet("game")]
        public IActionResult GetGameState([FromHeader] string userId)
        {
            Console.WriteLine($"Get state: {userId}.");
            _state.SpillDict();
            if (_state.ActiveGames.ContainsKey(userId))
            {
                Console.WriteLine($"Found pkm game for {userId}.");
                return Ok(_state.ActiveGames[userId].ToOutput());
            }

            return NotFound(userId);
        }

        [HttpPost("game/new")]
        public IActionResult CreateNewGame([FromHeader] string userId)
        {
            PkmModel pkm = _data.GetRandomPkm();
            GameStateModel state = new()
            {
                PkmRef = pkm,
                IsDone = false,
                MoveNameKey = pkm.Moves.ToDictionary(x => x.Name.ToUpper(), x => x.Id),
                MoveStates = pkm.Moves.Select<MoveModel, MoveStateModel>(x => GetMoveState(x)).ToDictionary(x => x.Id),
                Lives = 10
            };
            int potential = 0;
            foreach (MoveStateModel move in state.MoveStates.Values)
            {
                potential += move.Points;
            }
            state.Stats.Potential = potential;
            state.Stats.Max = potential;

            _state.ActiveGames.AddOrUpdate(userId, state, (key, val) => state);
            return Ok(state.ToOutput());
        }

        private MoveStateModel GetMoveState(MoveModel pMove)
        {
            return new()
            {
                Id = pMove.Id,
                Name = pMove.Name.ToUpper(),
                IsAnswered = false,
                MoveRef = pMove,
                DamageClassHint = GetHintState(pMove.DamageClassHint),
                TypeHint = GetHintState(pMove.TypeHint),
                FlavorTextHint = GetHintState(pMove.FlavorTextHint),
                Points = 50
            };
        }

        private HintStateModel GetHintState(HintModel pHint)
        {
            return new()
            {
                Id = pHint.Id,
                Hint = pHint.Hint,
                HintType = pHint.HintType,
                IsRevealed = false
            };
        }

        [HttpPut("hint")]
        public IActionResult PutHint([FromHeader] string userId, [FromBody] HintRevealModel hintReveal)
        {
            Console.WriteLine($"UserId: {userId} / Hint Reveal: {hintReveal.MoveId} : {hintReveal.HintType}");

            if (!_state.ActiveGames.ContainsKey(userId))
            {
                Console.WriteLine($"No user id found: {userId}.");
                return NotFound(userId);
            }

            GameStateModel state = _state.ActiveGames[userId];
            if (!state.MoveStates.ContainsKey(hintReveal.MoveId))
            {
                return NotFound(hintReveal.MoveId);
            }

            MoveStateModel move = state.MoveStates[hintReveal.MoveId];
            if (move.IsAnswered)
            {
                return Ok(new HintRevealResultModel()
                {
                    IsAlreadyRevealed = false,
                    IsMoveAlreadyAnswered = true,
                    State = state.ToOutput()
                });
            }

            HintStateModel? hint = null;
            HintModel? hintRef = null;
            switch (hintReveal.HintType.ToUpper()) 
            {
                case "DAMAGECLASS":
                    hint = move.DamageClassHint;
                    hintRef = move.MoveRef.DamageClassHint;
                    break;
                case "TYPE":
                    hint = move.TypeHint;
                    hintRef = move.MoveRef.TypeHint;
                    break;
                case "FLAVORTEXT":
                    hint = move.FlavorTextHint;
                    hintRef = move.MoveRef.FlavorTextHint;
                    break;
            }

            if (hint is null || hintRef is null)
            {
                return BadRequest(hintReveal.HintType);
            }

            if (hint.IsRevealed)
            {
                return Ok(new HintRevealResultModel()
                {
                    IsAlreadyRevealed = true,
                    IsMoveAlreadyAnswered = false,
                    State = state.ToOutput()
                });
            }

            hint.IsRevealed = true;

            //state.Stats.Score -= hintRef.ScoreCost;
            state.Stats.Potential -= hintRef.ScoreCost;
            move.Points -= hintRef.ScoreCost;

            return Ok(new HintRevealResultModel()
            {
                IsAlreadyRevealed = false,
                IsMoveAlreadyAnswered = false,
                State = state.ToOutput()
            });
        }

        [HttpPut("guess")]
        public IActionResult PutGuess([FromHeader] string userId, [FromBody] GuessModel pGuess)
        {
            int? moveId = null;
            string guess = pGuess.Guess.ToUpper();
            Console.WriteLine($"UserId: {userId} / Guess: {guess}.");
            _state.SpillDict();
            
            if (!_state.ActiveGames.ContainsKey(userId))
            {
                Console.WriteLine($"No user id found: {userId}.");
                return NotFound(userId);
            }

            GameStateModel state = _state.ActiveGames[userId];

            if (state.Guesses.Contains(guess)) 
            {
                return Ok(new GuessResultModel()
                {
                    IsDuplicate = true,
                    State = state.ToOutput()
                });
            }

            state.Stats.Guesses++;

            if (state.MoveNameKey.ContainsKey(guess))
            {
                Console.WriteLine($"Found move: {guess}");
                moveId = state.MoveNameKey[guess];
                state.Stats.Correct++;
                state.Stats.Score += state.MoveStates[moveId.Value].Points;
                state.MoveStates[moveId.Value].IsAnswered = true;
                state.MoveStates[moveId.Value].DamageClassHint.IsRevealed = true;
                state.MoveStates[moveId.Value].TypeHint.IsRevealed = true;
                state.MoveStates[moveId.Value].FlavorTextHint.IsRevealed = true;

                if (state.MoveStates.Values.All(x => x.IsAnswered))
                {
                    state.IsDone = true;
                    state.IsWin = true;
                }
            }
            else
            {
                state.WrongGuesses.Add(guess);
                state.Lives--;

                if (state.Lives == 0)
                {
                    state.IsDone = true;
                    foreach (MoveStateModel move in state.MoveStates.Values)
                    {
                        move.IsAnswered = true;
                        move.DamageClassHint.IsRevealed = true;
                        move.TypeHint.IsRevealed = true;
                        move.FlavorTextHint.IsRevealed = true;
                    }
                }
            }

            state.Guesses.Add(guess);

            return Ok(new GuessResultModel()
            {
                IsCorrect = moveId is not null,
                MoveId = moveId,
                State = state.ToOutput()
            });
        }

        [HttpPost("user")]
        public IActionResult CreateUser()
        {
            
            CreateUserResult user = new()
            {
                UserId = Guid.NewGuid().ToString()
            };

            Console.WriteLine($"POST: /user: {user.UserId}");

            return Ok(user);
        }

        [HttpPost("user/test/{id}")]
        public IActionResult TestUser(string id)
        {
            // Just to test oncurrency with user ids.
            Console.WriteLine("USERID: " + id);
            return Ok();
        }

        public class CreateUserResult
        {
            public string UserId { get; set; }
        }
    }
}
