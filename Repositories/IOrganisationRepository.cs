using System.Collections.Generic;
using AmberTestApp.Models;

namespace AmberTestApp.Repositories
{
    internal interface IOrganisationRepository
    {
        IEnumerable<Organisation> GetOrganisations(string userEmailAddress);
    }
}