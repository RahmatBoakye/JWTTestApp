using AmberTestApp.Models;
using System.Collections.Generic;

namespace AmberTestApp.Repositories
{
    //this will be a broker class
    //which will get some info about the certificate (e.g.: The thumbprint, description, etc)
    //fom the db
    public class CertificateRepository : ICertificateRepository
    {
        public IEnumerable<CertificateInfo> GetCertificateInformation()
        {
            return new List<CertificateInfo>()
            {
                new CertificateInfo()
                {
                    Thumbprint = "CC27D5CF9F001EB09B7E68855A9B052D07E57E4E"
                }
            };
        }
    }
}