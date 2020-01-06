namespace AmberTestApp.Services
{
    internal interface IAuthenticationService
    {
        bool ValidateUser(string userEmailAddress, string password);
    }
}
