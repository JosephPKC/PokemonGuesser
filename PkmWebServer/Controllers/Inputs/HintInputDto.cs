using PkmWebServer.Dtos.Hint;

namespace PkmWebServer.Controllers.Inputs
{
    public class HintInputDto
    {
        public int MoveId { get; set; }
        public HintTypes HintType { get; set; }
    }
}
