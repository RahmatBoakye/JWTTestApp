using AmberTestApp.Providers;
using System.Web.Mvc;

namespace AmberTestApp.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ICertificateProvider _certificateProvider;

        public CertificateController()
        {
            _certificateProvider = new CertificateProvider();
        }

        // GET: Certificate
        [HttpGet, Route("Certificate/GetKey")]
        public JsonResult GetCertificatePublicKey()
        {
            var publicKey = _certificateProvider.GetX509CertificatePublicKey(_certificateProvider.GetX509Ceritifcate());
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = publicKey.Key.ToXmlString(false)
            };
        }
    }
}