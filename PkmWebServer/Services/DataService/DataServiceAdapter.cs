using System.Collections.Concurrent;
using PkmWebServer.Controllers.Services;
using PkmWebServer.Models.Refs;
using PkmWebServer.TestData;

namespace PkmWebServer.Services.DataService;
public class DataServiceAdapter(IPkmDataApi pApi) : IDataService
{
    private readonly Random _rand = new();

    private readonly IPkmDataApi _data = pApi;

    private readonly ConcurrentDictionary<int, PkmRefModel> _pkmCache = [];

    private List<int>? _allIds = null;
    private List<int> AllIds
    {
        get
        {
            if (_allIds is null)
            {
                PkmAllApiModel? res = _data.GetAllPkm();
                _allIds = [.. res?.Ids ?? []];
            }

            return _allIds;
        }
    }

    #region IDataService
    public PkmRefModel GetRandomPkm()
    {
        int index = _rand.Next(0, AllIds.Count);
        int id = AllIds[index];

        if (_pkmCache.TryGetValue(id, out PkmRefModel? refModel))
        {
            return refModel;
        }

        PkmApiModel? apiModel = _data.GetPkmById(id) ?? throw new Exception($"Pkm id {id} not found.");
        refModel = PkmRefMapper.MapToRef(apiModel);
        _pkmCache.AddOrUpdate(id, refModel, (key, model) => refModel); // Just always update with this one if there is a conflict.

        return refModel;
    }
    #endregion
}
