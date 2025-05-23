using System.Text;
using PkmGuessGame;
using PkmGuessGame.Models.Inputs;
using PkmGuessGame.Models.Results;

namespace GuessGameCLI
{
    // TODO: We need the game manager to return the state, or find some way to get the udpated state.
    // Instead of having the caller manage and handle the state.
    // This means we won't have to replicate a lot of logic in the api gateway and front end.
    public static class Program
    {
        private static GuessGameManager _game = new();

        private static GameDisplayState _displayState = new();

        private static PkmInputsModel? _inputs = null;
        private static PkmInputsModel Inputs
        {
            get
            {
                _inputs ??= new()
                {
                    Abilities = _abilities,
                    Moves = _moves,
                    OldMoves = _oldMoves
                };

                return _inputs;
            }

        }

        private static IEnumerable<MoveInputModel> _moves = [
            new()
            {
                Id = 1,
                Name = "Tackle",
                Type = "Normal",
                FlavorText = "It is tackle.",
                DamageClass = "Physical"
            },
            new() {
                Id = 2,
                Name = "Ember",
                Type = "Fire",
                FlavorText = "It is ember.",
                DamageClass = "Special"
            },
            new() {
                Id = 3,
                Name = "Water Gun",
                Type = "Water",
                FlavorText = "It is water gun.",
                DamageClass = "Special"
            }
        ];

        private static IEnumerable<AbilityInputModel> _abilities = [
            new() {
                Id = 1,
                Name = "Overgrow",
                FlavorText = "It is overgrow."
            },
            new() {
                Id = 2,
                Name = "Torrent",
                FlavorText = "It is torrent."
            }
        ];

        private static IEnumerable<OldMoveInputModel> _oldMoves = [
            new() {
                Id = 100,
                Name = "String Shot"
            },
            new() {
                Id = 101,
                Name = "Iron Defense"
            },
            new() {
                Id = 102,
                Name = "Surf"
            }
        ];

        public class GameDisplayState
        {
            public List<AbilityDisplayState> Abilities { get; set; } = [];
            public List<MoveDisplayState> Moves { get; set; } = [];
            public List<OldMoveDisplayState> OldMoves { get; set; } = [];
        }

        public class AbilityDisplayState
        {
            public int Id { get; set; }
            public NameState Name { get; set; } = new();
            public HintState FlavorTextHint { get; set; } = new();
        }

        public class MoveDisplayState
        {
            public int Id { get; set; }
            public NameState Name { get; set; } = new();
            public HintState ClassHint { get; set; } = new();
            public HintState TypeHint { get; set; } = new();
            public HintState FlavorTextHint { get; set; } = new();
        }

        public class OldMoveDisplayState
        {
            public int Id { get; set; }
            public NameState Name { get; set; } = new();
        }

        public class NameState
        {
            public string Name { get; set; } = string.Empty;
            public bool IsHidden { get; set; } = true;
        }
        
        public class HintState
        {
            public string Hint { get; set; } = string.Empty;
            public int Cost { get; set; } = 0;
            public bool IsHidden { get; set; } = true;
        }

        private const string HiddenString = "*****";

        private static bool _isGameRunning = false;
        
        public static void Main()
        {
            while (true)
            {
                Console.Write("Enter: ");
                string? input = Console.ReadLine();
                string output = ProcessCommand(input);
                Console.Clear();
                Console.WriteLine(GetDisplayState());
                Console.WriteLine(output);
            }
        }

        private static string ProcessCommand(string? pInput)
        {
            if (string.IsNullOrWhiteSpace(pInput))
            {
                return GetErrorOutputString("Invalid input.");
            }

            (string?, string[]?) parsedInput = ParseInput(pInput);
            
            if (string.IsNullOrWhiteSpace(parsedInput.Item1))
            {
                return GetErrorOutputString("Invalid input format.");
            }

            string command = parsedInput.Item1;
            string[] args = parsedInput.Item2 ?? [];

            switch (command)
            {
                case "NG":
                    return StartNewGame();
                case "G":
                    return MakeGuess(args);
                case "H":
                    return RevealHint(args);
                default:
                    return GetErrorOutputString($"Invalid command: {command}.");
            }
        }

        private static (string?, string[]?) ParseInput(string pInput)
        {
            // Format is: Command [arg...]
            string[] split = pInput.ToUpper().Split(" ");
            if (split.Length == 0)
            {
                return (null, null);
            }

            if (split.Length == 1)
            {
                return (split[0], null);
            }

            return (split[0], split.Skip(1).ToArray());
        }

        private static string GetErrorOutputString(string pError)
        {
            return pError;
        }

        private static string StartNewGame()
        {
            _game.NewGame(Inputs);
            // Build out the inputs list
            GameDisplayState displayState = new();

            foreach (AbilityInputModel ability in Inputs.Abilities)
            {
                displayState.Abilities.Add(new()
                {
                    FlavorTextHint = new()
                    {
                        Cost = 5, // Need to get from api somewhere
                        Hint = ability.FlavorText
                    },
                    Id = ability.Id,
                    Name = new()
                    {
                        Name = ability.Name
                    }
                });
            }

            foreach (MoveInputModel move in Inputs.Moves)
            {
                displayState.Moves.Add(new()
                {
                    ClassHint = new()
                    {
                        Cost = 1,
                        Hint = move.DamageClass
                    },
                    FlavorTextHint = new()
                    {
                        Cost = 5,
                        Hint = move.FlavorText
                    },
                    Id = move.Id,
                    Name = new()
                    {
                        Name = move.Name
                    },
                    TypeHint = new()
                    {
                        Cost = 2,
                        Hint = move.Type
                    }
                });
            }

            foreach (OldMoveInputModel oldMove in Inputs.OldMoves)
            {
                displayState.OldMoves.Add(new()
                {
                    Id = oldMove.Id,
                    Name = new()
                    {
                        Name = oldMove.Name
                    }
                });
            }

            _isGameRunning = true;
            _displayState = displayState;
            return "Started New Game!";
        }

        private static string MakeGuess(string[] pArgs)
        {
            // Only one arg allowed.
            string guess = string.Join(" ", pArgs);
            GuessResultModel result = _game.ProcessGuess(guess);

            if (result.Result == GuessResultTypes.Correct)
            {
                if (result.GuessType == GuessTypes.Ability)
                {
                    AbilityDisplayState? state = _displayState.Abilities.Find(x => x.Id == result.GuessId);
                    if (state is null)
                    {
                        return $"Error: Could not find ability {result.GuessId}.";
                    }

                    state.Name.IsHidden = false;
                    state.FlavorTextHint.IsHidden = false;
                }
            }
            return $"{result.Result}!";
        }

        private static string RevealHint(string[] pArgs)
        {
            // h type type pos-index
            return "";
        }

        private static string GetDisplayState()
        {
            StringBuilder str = new();
            str.AppendLine($"| [1]  Ability | [2] Flavor Text | [3] Pts |");
            foreach (AbilityDisplayState ability in _displayState.Abilities)
            {
                string name = ability.Name.IsHidden ? HiddenString : ability.Name.Name;
                string hint = ability.FlavorTextHint.IsHidden ? $"Reveal [-{ability.FlavorTextHint.Cost} pts]" : ability.FlavorTextHint.Hint;
                string score = "10 POT"; // Need to get from api
                string line = $"| [1] {name} | [2] {hint} | [3] {score} |";
                str.AppendLine(line);
            }

            return str.ToString();
        }
    }
}