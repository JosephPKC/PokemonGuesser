using LogWrapper;

using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers
{
    internal class GetAllVersGrpIdsController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, int pCurrGenId)
        : BaseController(pApi, pCache, pLoggerConf, pCurrGenId)
    {
        public ISet<int> GetAllVersGrpIds()
        {
            HashSet<int> versGrpIds = [];

            GenerationRetModel? genRet = GetRetById<GenerationRetModel>(_currGenId);
            if (genRet is null)
            {
                return versGrpIds;
            }

            foreach (string versGrpUrl in genRet.VersionGroupResUrls)
            {
                RetrievalUtils.AddIdFromUrlIfExists(versGrpIds, versGrpUrl);
            }

            return versGrpIds;
        }


    }
}
