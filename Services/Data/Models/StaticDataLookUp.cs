using Data.Models.MoveDamageClass;
using Data.Models.Type;

namespace Data.Models;
internal class StaticDataLookUp
{
    public IDictionary<string, MoveDamageClassDataModel> MoveDamageClasses { get; set; } = new Dictionary<string, MoveDamageClassDataModel>();
    public IDictionary<string, TypeDataModel> Types { get; set; } = new Dictionary<string, TypeDataModel>();
}
