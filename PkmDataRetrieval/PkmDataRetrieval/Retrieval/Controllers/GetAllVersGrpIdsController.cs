using StackExchange.Redis;

using PkmDataRetrieval.Retrieval.Models.Generation;

namespace PkmDataRetrieval.Retrieval.Controllers
{
    internal class GetAllVersGrpIdsController(IPkmGateway pApi, IConnectionMultiplexer pConn, string pServiceKeyPrefix, int pCurrGenId)
        : BaseController(pApi, pConn, pServiceKeyPrefix, pCurrGenId)
    {
        public ISet<int> GetAllVersGrpIds()
        {
            HashSet<int> versGrpIds = [];

            GenerationRetModel? genRet = GetRetById<GenerationRetModel>(_currGenId);
            if (genRet is null)
            {
                //  WARN
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
