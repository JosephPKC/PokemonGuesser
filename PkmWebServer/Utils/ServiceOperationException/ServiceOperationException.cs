namespace PkmWebServer.Utils.ServiceOperationException;
public class ServiceOperationException(string? pMessage, ExceptionFaultTypes pType = ExceptionFaultTypes.Misc) : Exception(pMessage)
{
    public ExceptionFaultTypes FaultType { get; init; } = pType;
}
