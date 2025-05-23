namespace PkmWebServer.Controllers.Results.User;
public static class CreateUserResultMapper
{
    public static CreateUserResultDto CreateResult(string pUserId)
    {
        return new()
        {
            UserId = pUserId
        };
    }
}
