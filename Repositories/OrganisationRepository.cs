using AmberTestApp.Models;
using System;
using System.Collections.Generic;

namespace AmberTestApp.Repositories
{
    public class OrganisationRepository : IOrganisationRepository
    {
        ///this will be a broker class in Amber
        //which will get the organisation ids
        //return some in memory data.
        public IEnumerable<Organisation> GetOrganisations(string userEmailAddress)
        {
            var organisations = new List<Organisation>();

            if (!string.IsNullOrWhiteSpace(userEmailAddress))
            {
                organisations = new List<Organisation>
                {
                    new Organisation
                    {
                        OrganisationId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e")
                    },
                    new Organisation
                    {
                        OrganisationId = new Guid("6b945af9-2884-4ae5-9612-9c282da1bb40")
                    },
                     new Organisation
                    {
                        OrganisationId = new Guid("692cd915-225d-4307-9ba3-6004645d1a9f")
                    },
                      new Organisation
                    {
                        OrganisationId = new Guid("04689008-6936-4b3d-b531-fc6ff0637f8a")
                    }
                };
            }

            return organisations;
        }
    }
}