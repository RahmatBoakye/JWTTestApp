using AmberTestApp.Repositories;

namespace AmberTestApp.Services
{
    //this bit will do the authentication to check if
    //that user email and password credentials are valid.
    //I'm sure there is one in Amber we could reuse.
    //Todo: Check if there is a reusable Authentication Class in Amber to use.
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
        }

        public bool ValidateUser(string userEmailAddress, string password)
        {
            var user = _userRepository.FindUserByEmailAndPassword(userEmailAddress, password);
            return user == null ? false : true;
        }
    }
}