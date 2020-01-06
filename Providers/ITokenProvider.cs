using AmberTestApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace AmberTestApp.Providers
{
    internal interface ITokenProvider
    {
        bool IsTokenValid(string token, X509Certificate certificate);

        string GenerateToken(ITokenModel tokenModel);

        IEnumerable<Claim> GetTokenClaims(string token, X509Certificate certificate);
        
    }
}
