using PkmWebApi.Models.Refs;
using PkmWebApi.TestData;

namespace PkmWebApi.Services.DataService
{
    public static class PkmRefMapper
    {
        public static PkmRefModel MapToRef(PkmApiModel pModel)
        {
            (string?, string?) types = GetPkmTypes(pModel);
            return new()
            {
                Id = pModel.Id,
                Name = pModel.Name.Name,
                Type1 = types.Item1 ?? "",
                Type2 = types.Item2,
                Moves = GetMoves(pModel)
            };
        }

        private static (string?, string?) GetPkmTypes(PkmApiModel pModel)
        {
            List<NameApiModel> types = [.. pModel.Types];

            if (types.Count == 0)
            {
                return (null, null);
            }

            if (types.Count == 1)
            {
                return (types[0].Name, null);
            }

            return (types[0].Name, types[1].Name);
        } 

        private static Dictionary<int, MoveRefModel> GetMoves(PkmApiModel pModel)
        {
            Dictionary<int, MoveRefModel> moves = [];
            foreach (PkmMoveApiModel move in  pModel.Moves)
            {
                if (moves.ContainsKey(move.Id))
                {
                    continue;
                }
                moves.Add(move.Id, MoveRefMapper.MapToRef(move));
            }
            return moves;
        }
    }
}
