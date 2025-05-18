using PkmWebServer.Controllers.Services;

namespace PkmWebServer.Services.UserService;
public class UserServiceAdapter(IActiveGameRepo pRepo) : IUserService
{
    private readonly IActiveGameRepo _activeGames = pRepo;

    #region IUserService
    public string CreateNewUser()
    {
        string userId = Guid.NewGuid().ToString();
        //_activeGames.AddOrUpdate(userId, new(), (id, state) => new());
        return userId;
    }

    public bool ValidateUser(string pUserId)
    {
        return _activeGames.ContainsKey(pUserId);
    }
    #endregion
}
