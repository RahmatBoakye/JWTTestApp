using AmberTestApp.Models;

namespace AmberTestApp.Repositories
{
    internal interface IUserRepository
    {
        AmberUser FindUserByEmailAndPassword(string userEmail, string password);
    }
}
