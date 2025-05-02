using Microsoft.AspNetCore.Mvc;

using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Api.Models.Pokemon;

namespace PkmDataRetrieval.Api
{
    [ApiController]
    [Route("api/data-retrieval")]
    public class DataRetrievalController(IDataRetrieval pDataRetriever, LogWrapper.Loggers.ILoggerFactory pLogFactory) : ControllerBase
    {
        private readonly IDataRetrieval _dataRetriever = pDataRetriever;
        private readonly LogWrapper.Loggers.ILogger log = pLogFactory.CreateNewLogger(typeof(DataRetrievalController));

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
                log.Warn(ex.Message);
            }

            if (result is null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }
    }
}
