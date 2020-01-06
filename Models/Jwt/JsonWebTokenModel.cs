using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace AmberTestApp.Models
{
    public class JsonWebTokenModel : ITokenModel
    {
        public X509Certificate Certificate { get; set; }
        
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.RsaSha256Signature;

        public string DigestAlgorithm { get; set; } = SecurityAlgorithms.Sha256Digest;

        //token expires in 7 days.
        public int ExpireMinutes { get; set; } = 10080; 

        public IEnumerable<Claim> Claims { get; set; }
    }
}