namespace PkmGuessGame.Models.States
{
    internal class StateDict
    {
        private readonly Dictionary<string, int> _nameKeys = [];
        private readonly Dictionary<int, GuessStateModel> _states = [];

        public int this[string pNameKey]
        {
            get => _nameKeys[pNameKey];
            set => _nameKeys[pNameKey] = value;
        }

        public GuessStateModel this[int pId]
        {
            get => _states[pId];
            set => _states[pId] = value;
        }

        public void Add(string pNameKey, int pId, GuessStateModel pState)
        {
            if (_nameKeys.ContainsKey(pNameKey))
            {
                return;
            }

            _nameKeys.Add(pNameKey, pId);

            if (_states.ContainsKey(pId))
            {
                return;
            }

            _states.Add(pId, pState);
        }

        public void Add<TItem>(IEnumerable<TItem> pItemsToAdd, Func<TItem, string?> pGetNameKey, Func<TItem, int?> pGetId, Func<TItem, GuessStateModel?> pGetState)
        {
            foreach (TItem item in pItemsToAdd)
            {
                string? nameKey = pGetNameKey(item);
                if (nameKey is null)
                {
                    continue;
                }

                int? id = pGetId(item);
                if (id is null)
                {
                    continue;
                }

                GuessStateModel? state = pGetState(item);
                if (state is null)
                {
                    continue;
                }

                Add(nameKey, id.Value, state);
            }
        }

        public void Clear()
        {
            _nameKeys.Clear();
        }

        public bool Contains(string pNameKey)
        {
            return _nameKeys.ContainsKey(pNameKey);
        }

        public bool Contains(int pId)
        {
            return _states.ContainsKey(pId);
        }

        public int Count()
        {
            return _nameKeys.Count;
        }

        public bool Empty()
        {
            return _nameKeys.Count == 0 && _states.Count == 0;
        }

        public int? GetId(string pNameKey)
        {
            if (_nameKeys.TryGetValue(pNameKey, out int id))
            {
                return id;
            }

            return null;
        }

        public GuessStateModel? GetState(string pNameKey)
        {
            int? id = GetId(pNameKey);
            if (id is null)
            {
                return null;
            }

            return GetState(id.Value);
        }

        public GuessStateModel? GetState(int pId)
        {
            if (_states.TryGetValue(pId, out GuessStateModel? state))
            {
                return state;
            }

            return null;
        }

        public void Remove(string pNameKey)
        {
            if (!_nameKeys.TryGetValue(pNameKey, out int id))
            {
                return;
            }

            if (!_states.ContainsKey(id))
            {
                return;
            }

            _nameKeys.Remove(pNameKey);
            _states.Remove(id);
        }
    }
}
