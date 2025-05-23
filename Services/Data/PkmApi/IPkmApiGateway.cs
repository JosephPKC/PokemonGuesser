using Data.Models;
using Data.Models.Basic;

namespace Data.PkmApi;
public interface IPkmApiGateway
{
    BasicLiDataModel? GetAll<TData>() where TData : class, IDataModel;
    TData? GetById<TData>(int pId) where TData : class, IDataModel;
}
