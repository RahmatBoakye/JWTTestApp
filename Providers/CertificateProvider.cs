using AmberTestApp.Repositories;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AmberTestApp.Providers
{
    public class CertificateProvider : ICertificateProvider
    {
        //Todo: Register this interface with an IoC container.
        private readonly ICertificateRepository _repository;

        public CertificateProvider()
        {
            _repository = new CertificateRepository();
        }

        public X509Certificate GetX509Ceritifcate()
        {
            var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            var certificate = new X509Certificate2();

            certStore.Open(OpenFlags.ReadOnly);
            var certificateCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, GetCertificateThumbPrint(), false);
            if (certificateCollection.Count == 1 && certificateCollection[0] != null)
            {
                certificate = certificateCollection[0];
            }

            return certificate;
        }

        public AsymmetricAlgorithm GetX509CertificatePrivateKey(X509Certificate certificate)
        {
            var signingCert = (X509Certificate2)certificate;
            return signingCert.PrivateKey;
        }

        public PublicKey GetX509CertificatePublicKey(X509Certificate certificate)
        {
            var signingCert = (X509Certificate2)certificate;
            return signingCert.PublicKey;
        }

        public bool IsPublicKeyAvailable(X509Certificate certificate)
        {
            var signingCert = (X509Certificate2)certificate;
            if (signingCert.PublicKey == null)
            {
                return false;
            }
                
            return true;
        }

        private string GetCertificateThumbPrint()
        {
            var certData = _repository.GetCertificateInformation();
            var certThumbPrint = string.Empty;

            if (certData.Any() && certData.Count() == 1)
            {
                certThumbPrint = certData.FirstOrDefault().Thumbprint;
            }

            return certThumbPrint;
        }
    }
}