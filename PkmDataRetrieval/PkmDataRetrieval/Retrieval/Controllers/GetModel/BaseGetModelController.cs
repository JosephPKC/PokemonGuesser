using StackExchange.Redis;

using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Retrieval.Models.Meta;

namespace PkmDataRetrieval.Retrieval.Controllers.GetModel
{
    internal abstract class BaseGetModelController(IPkmGateway pApi, IConnectionMultiplexer pConn, KeyPrefixes pKeyPrefixes, CurrentIds pCurrentIds, StaticDataCont pStaticData)
        : BaseController(pApi, pConn, pKeyPrefixes.ServiceKeyPrefix, pCurrentIds.CurrentGenId)
    {
        protected readonly StaticDataCont _staticData = pStaticData;
        protected readonly string _actionKeyPrefix = pKeyPrefixes.ActionKeyPrefix;
        protected readonly ISet<int> _currVersGrpIds = pCurrentIds.CurrentVersGrpIds;

        protected static bool IsAltForm(string pName)
        {
            //  TODO: Need a better way of determining alt forms.
            //  For now, hardcoded to include all paldean forms only.
            return pName.Contains("PALDEA", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
