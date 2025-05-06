namespace PkmGuessGame
{
    // Basically, a multi dictionary for pkm moves
    // Moves are categorized by their move learn method
    // Moves also need to be identifiable by both the name key and id.
    public class MoveStateDict
    {
        protected readonly Dictionary<string, int> _allMoves = [];

        protected readonly Dictionary<string, int> _levelMoves = [];
        protected readonly Dictionary<int, MoveState> _levelMoveStates = [];

        protected readonly Dictionary<string, int> _machineMoves = [];
        protected readonly Dictionary<int, MoveState> _machineMoveStates = [];

        protected readonly Dictionary<string, int> _eggMoves = [];
        protected readonly Dictionary<int, MoveState> _eggMoveStates = [];

        protected readonly Dictionary<string, int> _tutorMoves = [];
        protected readonly Dictionary<int, MoveState> _tutorMoveStates = [];

        public bool Add(string pLearnMethod, string pNameKey, int pId, MoveState pMoveState)
        {
            return Add(GetMoveLearnMethod(pLearnMethod), pNameKey, pId, pMoveState);
        }

        public bool Add(MoveLearnMethods pLearnMethod, string pNameKey, int pId, MoveState pMoveState)
        {
            Dictionary<string, int> moves = GetDict(pLearnMethod);
            if (moves.TryGetValue(pNameKey, out _))
            {
                return false;
            }

            Dictionary<int, MoveState> moveStates = GetStateDict(pLearnMethod);
            if (moveStates.TryGetValue(pId, out _))
            {
                return false;
            }

            moves.Add(pNameKey, pId);
            moveStates.Add(pId, pMoveState);

            if (!_allMoves.TryGetValue(pNameKey, out _))
            {
                _allMoves.Add(pNameKey, pId);
            }

            return true;
        }

        public void Clear()
        {
            _allMoves.Clear();
            _levelMoves.Clear();
            _levelMoveStates.Clear();
            _machineMoves.Clear();
            _machineMoveStates.Clear();
            _eggMoves.Clear();
            _eggMoveStates.Clear();
            _tutorMoves.Clear();
            _tutorMoveStates.Clear();
        }

        public bool Contains(string pNameKey)
        {
            return _allMoves.ContainsKey(pNameKey);
        }


        public int? GetId(string pNameKey)
        {
            if (_allMoves.TryGetValue(pNameKey, out int id))
            {
                return id;
            }

            return null;
        }

        public MoveState? GetState(string pLearnMethod, string pNameKey)
        {
            return GetState(GetMoveLearnMethod(pLearnMethod), pNameKey);
        }

        public MoveState? GetState(MoveLearnMethods pLearnMethod, string pNameKey)
        {
            int? id = GetId(pNameKey);
            if (id.HasValue)
            {
                return GetState(pLearnMethod, id.Value);
            }

            return null;
        }

        public MoveState? GetState(string pLearnMethod, int pId)
        {
            return GetState(GetMoveLearnMethod(pLearnMethod), pId);
        }

        public MoveState? GetState(MoveLearnMethods pLearnMethod, int pId)
        {
            Dictionary<int, MoveState> moveStates = GetStateDict(pLearnMethod);

            if (moveStates.TryGetValue(pId, out MoveState? state))
            {
                return state;
            }

            return null;
        }

        protected Dictionary<string, int> GetDict(MoveLearnMethods pLearnMethod)
        {
            return pLearnMethod switch
            {
                MoveLearnMethods.LevelUp => _levelMoves,
                MoveLearnMethods.Machine => _machineMoves,
                MoveLearnMethods.Egg => _eggMoves,
                MoveLearnMethods.Tutor => _tutorMoves,
                _ => []
            };
        }

        protected Dictionary<int, MoveState> GetStateDict(MoveLearnMethods pLearnMethod)
        {
            return pLearnMethod switch
            {
                MoveLearnMethods.LevelUp => _levelMoveStates,
                MoveLearnMethods.Machine => _machineMoveStates,
                MoveLearnMethods.Egg => _eggMoveStates,
                MoveLearnMethods.Tutor => _tutorMoveStates,
                _ => []
            };
        }

        protected MoveLearnMethods GetMoveLearnMethod(string pMethod)
        {
            return pMethod.ToUpper() switch
            {
                "LEVEL-UP" => MoveLearnMethods.LevelUp,
                "MACHINE" => MoveLearnMethods.Machine,
                "EGG" => MoveLearnMethods.Egg,
                "TUTOR" => MoveLearnMethods.Tutor,
                _ => MoveLearnMethods.LevelUp
            };
        }
    }

    public class MoveState
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CurrentScore { get; set; }
        public Dictionary<MoveHintTypes, MoveHint> Hints { get; set; } = [];
        public bool IsAnswered { get; set; }
    }

    public class MoveHint
    {
        public string Hint { get; set; } = string.Empty;
        public int ScoreCost { get; set; }
        public bool IsRevealed { get; set; }
    }

    public enum MoveHintTypes
    {
        Type,
        DamageClass,
        FlavorText
    }

    public enum MoveLearnMethods
    {
        LevelUp,
        Machine,
        Egg,
        Tutor
    }
}
