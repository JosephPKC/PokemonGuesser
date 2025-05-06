namespace PkmGuessGame
{
    public class OldMoveStateDict
    {
        protected readonly Dictionary<string, int> _oldMoves = [];

        public bool Add(string pNameKey, int pId)
        {
            if (_oldMoves.TryGetValue(pNameKey, out _))
            {
                return false;
            }

            _oldMoves.Add(pNameKey, pId);
            return true;
        }

        public void Clear()
        {
            _oldMoves.Clear();
        }
    }
}
