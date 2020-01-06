using AmberTestApp.Models;

namespace AmberTestApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        public AmberUser FindUserByEmailAndPassword(string userEmail, string password)
        {
            if (!string.IsNullOrWhiteSpace(userEmail)
                && !string.IsNullOrWhiteSpace(password))
            {
                return new AmberUser()
                {
                    UserEmailAddress = userEmail,
                    Password = password
                };
            }

            return null;
        }
    }
}