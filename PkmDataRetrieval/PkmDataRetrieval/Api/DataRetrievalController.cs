using Microsoft.AspNetCore.Mvc;

using PkmDataRetrieval.Api.Models.Generation;
using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Api
{
    [ApiController]
    [Route("api/data-retrieval")]
    public class DataRetrievalController : ControllerBase
    {
        private readonly IDataRetrieval _dataRetriever;

        public DataRetrievalController(IDataRetrieval pDataRetriever)
        {
            _dataRetriever = pDataRetriever;
        }

        [HttpGet("gen/current")]
        public IActionResult GetCurrentGen()
        {
            GenModel? result = _dataRetriever.GetCurrentGen();
            if (result is null)
            {
                return NotFound(Config.CurrentGenId);
            }

            return Ok(result);
        }

        [HttpGet("pkm")]
        public IActionResult GetAllPkm()
        {
            PkmAllModel? result = _dataRetriever.GetAllPkm();
            if (result is null)
            {
                return NotFound(Config.CurrentGenId);
            }

            return Ok(result);
        }

        [HttpGet("pkm/{id}")]
        public IActionResult GetPkmById(int id)
        {
            PkmModel? result = _dataRetriever.GetPkmById(id);
            if (result is null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }


    }
}
