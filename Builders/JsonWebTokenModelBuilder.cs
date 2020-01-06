using AmberTestApp.Providers;
using AmberTestApp.Models;
using AmberTestApp.Repositories;
using System.Collections.Generic;
using System.Security.Claims;

namespace AmberTestApp.Builders
{
    public class JsonWebTokenModelBuilder : IJsonWebTokenModelBuilder
    {
        private readonly IOrganisationRepository _repository;
        private readonly ICertificateProvider _provider;

        public JsonWebTokenModelBuilder()
        {
            _repository = new OrganisationRepository();
            _provider = new CertificateProvider();
        }

        public JsonWebTokenModel BuildModel(string email)
        {
            var organisationClaims = CreateOrganisationClaims(email);
            organisationClaims.Add(CreateEmailClaim(email));

            return new JsonWebTokenModel()
            {
                Claims = organisationClaims,
                Certificate = _provider.GetX509Ceritifcate()
            };
        }

        private List<Claim> CreateOrganisationClaims(string email)
        {
            var organisations = GetOrganisations(email);
            

            var claims = new List<Claim>();

            foreach (var item in organisations)
            {
                claims.Add(new Claim(ClaimTypes.Name, item.OrganisationId.ToString()));
            }

            return claims;
        }

        private Claim CreateEmailClaim(string email)
        {
            return new Claim(ClaimTypes.Email, email);
        }

        private IEnumerable<Organisation> GetOrganisations(string email)
        {
           return _repository.GetOrganisations(email);
        }
    }
}