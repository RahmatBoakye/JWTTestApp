using AmberTestApp.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AmberTestApp.Providers
{
    /// <summary>
    /// This class will be responsible for creating the signed token, verifying signature, 
    /// check token validity and get claims in token.
    /// </summary>
    public class JsonWebTokenProvider : ITokenProvider
    {
        /// <summary>
        /// Generates token by given model.
        /// Validates whether the given model is valid, then gets the private key.
        /// Encrypt the token and returns it.
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public string GenerateToken(ITokenModel tokenModel)
        {
            if (IsModelInValid(tokenModel))
                throw new ArgumentException("Arguments to create token are not valid");

            var signingCert = (X509Certificate2)tokenModel.Certificate;
            var signingKey = (RSACryptoServiceProvider)signingCert.PrivateKey;
            var rsaKey = new RsaSecurityKey(signingKey);
           

            var signingCredentials = new SigningCredentials(rsaKey, tokenModel.SecurityAlgorithm, tokenModel.DigestAlgorithm)
            {
                CryptoProviderFactory = new CryptoProviderFactory()
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = signingCert.Issuer,
                Audience = "Client",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(tokenModel.ExpireMinutes),
                Subject = new ClaimsIdentity(tokenModel.Claims),
                SigningCredentials = signingCredentials
            };

            var handler = new JsonWebTokenHandler();
            var jwt = handler.CreateToken(descriptor);

            var signatureProvider = signingCredentials.CryptoProviderFactory.CreateForSigning(rsaKey, tokenModel.SecurityAlgorithm);
            var jwtArray = jwt.Split('.');
            var encodedHeader = jwtArray[0];
            var encodedPayload = jwtArray[1];

            var input = string.Join(".", new[] { encodedHeader, encodedPayload });
            var signature = signatureProvider.Sign(Encoding.UTF8.GetBytes(input));

            signingCredentials.CryptoProviderFactory.ReleaseSignatureProvider(signatureProvider);
            var signedJwt = string.Join(".", new[] { encodedHeader, encodedPayload, Base64UrlEncoder.Encode(signature) });

            return signedJwt;
        }

        /// <summary>
        /// Receives the token as a string and returns all claims in it.
        /// </summary>
        /// <param name = "token" ></ param >
        /// < returns > IEnumerable of claims for the given token.</returns>
        public IEnumerable<Claim> GetTokenClaims(string token, X509Certificate certificate)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be null or empty");

            if (certificate == null)
                throw new ArgumentException("Certificate cannot be null or empty");

            var handler = new JsonWebTokenHandler();

            var tokenValidationResult = handler.ValidateToken(token, GetTokenValidationParameters(certificate));
            if (tokenValidationResult.IsValid && tokenValidationResult.ClaimsIdentity != null)
            {
                return tokenValidationResult.ClaimsIdentity.Claims;
            }

            return new List<Claim>();
        }

        //takes the token and checks if its invalid or has expired
        public bool IsTokenValid(string token, X509Certificate certificate)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token cannot be null or empty");
            }

            if (certificate == null)
            {
                throw new ArgumentException("Certificate cannot be null or empty");
            }

            var handler = new JsonWebTokenHandler();
            var tokenValidationResult = handler.ValidateToken(token, GetTokenValidationParameters(certificate));

            return tokenValidationResult.IsValid;
        }

        private SecurityKey GetAsymmetricSecurityKey(X509Certificate certificate)
        {
            var signingCert = (X509Certificate2)certificate;
            var signingKey = (RSACryptoServiceProvider)signingCert.PrivateKey;

            return new RsaSecurityKey(signingKey);
        }

        private TokenValidationParameters GetTokenValidationParameters(X509Certificate certificate)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetAsymmetricSecurityKey(certificate)
            };
        }

        private bool IsModelInValid(ITokenModel tokenModel)
        {
            return tokenModel == null
                || tokenModel.Claims == null
                || tokenModel.Claims.Count() == 0;
        }
    }
}