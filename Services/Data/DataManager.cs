using Data.Models.Api;
using Data.Models.Api.Pokemon;
using Data.Controllers;
using Data.Controllers.StaticData;
using Data.Controllers.GetModel;
using Data.PkmApi;
using Data.Utils;
using Data.Models;
using Data.Models.MoveDamageClass;
using Data.Models.Type;

namespace Data;
internal class DataManager: IDataManager
{
    private readonly GetAllMoveDamageClassesController _getAllMoveDmgCls;
    private readonly GetAllTypesController _getAllTypes;
    private readonly GetAllPkmController _getAllPkm;
    private readonly GetCurrentGenController _getCurrGen;
    private readonly GetPkmByIdController _getPkmById;

    public DataManager(IPkmApiGateway pApi, ICacheHandlerFactory pCacheHandlerFactory, ILogFactory pLogFactory, int pGenId)
    {
        ICacheHandler cache = pCacheHandlerFactory.CreateCacheHandler();
        GetAllVersGrpIdsController getAllVersGrpIds = new(pApi, cache, pLogFactory.CreateNewLogger(typeof(GetAllVersGrpIdsController)), pGenId);
        ISet<int> allVersGrpIds = getAllVersGrpIds.GetAllVersGrpIds();

        _getAllMoveDmgCls = new(pApi, cache, pLogFactory.CreateNewLogger(typeof(GetAllMoveDamageClassesController)), pGenId);
        _getAllTypes = new(pApi, cache, pLogFactory.CreateNewLogger(typeof(GetAllTypesController)), pGenId);

        StaticDataLookUp staticData = GetStaticData();

        _getAllPkm = new(pApi, cache, pLogFactory.CreateNewLogger(typeof(GetAllPkmController)), pGenId, allVersGrpIds, Config.PkmAllKeyPrefix, staticData);
        _getCurrGen = new(pApi, cache, pLogFactory.CreateNewLogger(typeof(GetCurrentGenController)), pGenId, allVersGrpIds, Config.GenByIdKeyPrefix, staticData);
        _getPkmById = new(pApi, cache, pLogFactory.CreateNewLogger(typeof(GetPkmByIdController)), pGenId, allVersGrpIds, Config.PkmByIdKeyPrefix, staticData);
    }

    #region IDataRetrieval
    public PkmAllApiModel? GetAllPkm()
    {
        return _getAllPkm.GetAllPkm();
    }

    public BasicApiModel? GetCurrentGen()
    {
        return _getCurrGen.GetCurrentGen();
    }

    public PkmApiModel? GetPkmById(int pId)
    {
        return _getPkmById.GetPkmById(pId);
    }
    #endregion

    private StaticDataLookUp GetStaticData()
    {
        return new()
        {
            MoveDamageClasses = _getAllMoveDmgCls.GetAllMoveDamageClasses() ?? new Dictionary<string, MoveDamageClassDataModel>(),
            Types = _getAllTypes.GetAllTypes() ?? new Dictionary<string, TypeDataModel>(),
        };
    }
}
