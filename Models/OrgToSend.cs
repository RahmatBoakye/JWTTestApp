using System;
using System.Collections.Generic;

namespace AmberTestApp.Models
{
    public class OrgToSend
    {
        public IEnumerable<Guid> OrganisationIds { get; set; }
        public IEnumerable<int> TenantIds { get; set; }

        public Dictionary<Guid,int> Organisations { get; set; }
    }
}