using AmberTestApp.Providers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace AmberTestApp.Models
{
    public interface ITokenModel
    {
        X509Certificate Certificate { get; set; }

        string SecurityAlgorithm { get; set; }

        string DigestAlgorithm { get; set; }

        int ExpireMinutes { get; set; }

        IEnumerable<Claim> Claims { get; set; }
    }
}
