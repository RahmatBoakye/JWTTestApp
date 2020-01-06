using AmberTestApp.Models;
using System.Collections.Generic;

namespace AmberTestApp.Repositories
{
    internal interface ICertificateRepository
    {
        IEnumerable<CertificateInfo> GetCertificateInformation();
    }
}
