using PkmDataRetrieval.Api;
using PkmDataRetrieval.Api.Models;
using PkmDataRetrieval.Api.Models.Meta;
using PkmDataRetrieval.Api.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models.Meta;
using LogWrapper;

using PkmDataRetrieval.Retrieval.Controllers;
using PkmDataRetrieval.Retrieval.Controllers.GetModel;
using PkmDataRetrieval.Retrieval.Controllers.StaticData;
using PkmDataRetrieval.Utils.Caching;

namespace PkmDataRetrieval.Retrieval
{
    /// <summary>
    /// The core of this service.
    /// It will get data from the external api and transform that data into something the service api endpoint uses.
    /// </summary>
    internal class PkmDataRetriever: IDataRetrieval
    {
        private readonly GetAllMoveDamageClassesController _getAllMoveDmgCls;
        private readonly GetAllMoveLearnMethodsController _getAllMoveLearnMeths;
        private readonly GetAllTypesController _getAllTypes;
        private readonly GetAllPkmController _getAllPkm;
        private readonly GetCurrentGenController _getCurrGen;
        private readonly GetPkmByIdController _getPkmById;

        public PkmDataRetriever(IPkmGateway pApi, ICacheHandler pCache, LogWrapper.Loggers.ILoggerFactory pLoggerFactory, int pGenId)
        {

            GetAllVersGrpIdsController getAllVersGrpIds = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetAllVersGrpIdsController), pLoggerFactory), pGenId);
            _getAllMoveDmgCls = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetAllMoveDamageClassesController), pLoggerFactory), pGenId);
            _getAllMoveLearnMeths = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetAllMoveLearnMethodsController), pLoggerFactory), pGenId);
            _getAllTypes = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetAllTypesController), pLoggerFactory), pGenId);

            CurrentIds currIds = new()
            {
                CurrentGenId = pGenId,
                CurrentVersGrpIds = getAllVersGrpIds.GetAllVersGrpIds()
            };

            StaticDataCont staticData = GetStaticData();

            _getAllPkm = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetAllPkmController), pLoggerFactory), Config.PkmAllKeyPrefix, currIds, staticData);
            _getCurrGen = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetCurrentGenController), pLoggerFactory), Config.GenByIdKeyPrefix, currIds, staticData);
            _getPkmById = new(pApi, pCache, GetLoggerFactoryConf(typeof(GetPkmByIdController), pLoggerFactory), Config.PkmByIdKeyPrefix, currIds, staticData);
        }

        #region IDataRetrieval
        public PkmAllModel? GetAllPkm()
        {
            return _getAllPkm.GetAllPkm();
        }

        public BasicModel? GetCurrentGen()
        {
            return _getCurrGen.GetCurrentGen();
        }

        public PkmModel? GetPkmById(int pId)
        {
            return _getPkmById.GetPkmById(pId);
        }
        #endregion

        private StaticDataCont GetStaticData()
        {
            return new()
            {
                MoveDamageClasses = _getAllMoveDmgCls.GetAllMoveDamageClasses() ?? new Dictionary<string, Models.MoveDamageClass.MoveDamageClassRetModel>(),
                MoveLearnMethods = _getAllMoveLearnMeths.GetAllMoveLearnMethods() ?? new Dictionary<string, Models.MoveLearnMethod.MoveLearnMethodRetModel>(),
                Types = _getAllTypes.GetAllTypes() ?? new Dictionary<string, Models.Type.TypeRetModel>(),
            };
        }

        private static LoggerFactoryConf GetLoggerFactoryConf(Type pDeclaringType, LogWrapper.Loggers.ILoggerFactory pLoggerFactory)
        {
            return new()
            {
                DeclaringType = pDeclaringType,
                LoggerFactory = pLoggerFactory
            };
        }
    }
}
