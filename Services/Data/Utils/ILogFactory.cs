namespace Data.Utils;
public interface ILogFactory
{
    ILog CreateNewLogger(Type pDeclaringType, bool pIsNull = false);
}
