using LogWrapper;

using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Retrieval.Models.Meta;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval.Controllers.GetModel
{
    internal abstract class BaseGetModelController(IPkmGateway pApi, ICacheHandler pCache, LoggerFactoryConf pLoggerConf, string pKeyPrefix, CurrentIds pCurrentIds, StaticDataCont pStaticData)
        : BaseController(pApi, pCache, pLoggerConf, pCurrentIds.CurrentGenId)
    {
        protected readonly StaticDataCont _staticData = pStaticData;
        protected readonly string _actionKeyPrefix = pKeyPrefix;
        protected readonly ISet<int> _currVersGrpIds = pCurrentIds.CurrentVersGrpIds;

        protected static bool IsAltForm(string pName)
        {
            //  TODO: Need a better way of determining alt forms.
            //  For now, hardcoded to include all paldean forms only.
            return pName.Contains("PALDEA", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
