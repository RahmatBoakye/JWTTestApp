using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AmberTestApp.Providers
{
    internal interface ICertificateProvider
    {
        X509Certificate GetX509Ceritifcate();

        AsymmetricAlgorithm GetX509CertificatePrivateKey(X509Certificate certificate);

        PublicKey GetX509CertificatePublicKey(X509Certificate certificate);

        bool IsPublicKeyAvailable(X509Certificate certificate);
    }
}
