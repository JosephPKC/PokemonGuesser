namespace PkmWebServer.Controllers.Results.User;
public static class ValidateUserResultMapper
{
    public static ValidateUserResultDto CreateResult(bool pUserExists)
    {
        return new()
        {
            UserExists = pUserExists
        };
    }
}
