using Microsoft.AspNetCore.Mvc;
using PkmDataRetrieval.Api.Models;
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
        [ProducesResponseType<BasicModel>(StatusCodes.Status200OK)]
        public IActionResult GetCurrentGen()
        {
            BasicModel? result = _dataRetriever.GetCurrentGen();
            if (result is null)
            {
                return NotFound(Config.CurrentGenId);
            }

            return Ok(result);
        }

        [HttpGet("pkm")]
        [ProducesResponseType<PkmAllModel>(StatusCodes.Status200OK)]
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
        [ProducesResponseType<PkmModel>(StatusCodes.Status200OK)]
        public IActionResult GetPkmById(int id)
        {
            PkmModel? result = null;

            try
            {
                result = _dataRetriever.GetPkmById(id);
            }
            catch (HttpRequestException ex)
            {
                //  WARN
            }

            if (result is null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }
    }
}
