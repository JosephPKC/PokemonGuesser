using PkmWebApi.Dtos.Hint;

namespace PkmWebApi.Dtos.Inputs
{
    public class HintInputDto
    {
        public int MoveId { get; set; }
        public HintTypes HintType { get; set; }
    }
}
