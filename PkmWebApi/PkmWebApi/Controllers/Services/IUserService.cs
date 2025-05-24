namespace PkmWebApi.Controllers.Services;
public interface IUserService
{
    string CreateNewUser();
    bool ValidateUser(string pUserId);
}
