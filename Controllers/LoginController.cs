using AmberTestApp.Builders;
using AmberTestApp.Models;
using AmberTestApp.Providers;
using AmberTestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace AmberTestApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IJsonWebTokenModelBuilder _tokenModelBuilder;
        private readonly ITokenProvider _tokenManager;
        private readonly IAuthenticationService _authenticationService;

        private static string RequestId = string.Empty;

        public LoginController()
        {
            _tokenModelBuilder = new JsonWebTokenModelBuilder();
            _tokenManager = new JsonWebTokenProvider();
            _authenticationService = new AuthenticationService();
        }

       [HttpGet, Route("Login/Index/")]
        public ActionResult Index(string id)
        {
            RequestId = id;
            return View();
        }

        [HttpPost, Route("Login/GetOrganisations/")]
        public JsonResult GetOrganisations(AmberUser amberUser)
        {
            var result = new string[2];

            if (_authenticationService.ValidateUser(amberUser.UserEmailAddress, amberUser.Password))
            {
                ITokenModel model = _tokenModelBuilder.BuildModel(amberUser.UserEmailAddress);

                string token = _tokenManager.GenerateToken(model);

                if (!_tokenManager.IsTokenValid(token, model.Certificate))
                {
                    throw new UnauthorizedAccessException("invalid token");
                }
                else
                {
                    result[0] = token;
                    result[1] = RequestId;

                    List<Claim> claims = _tokenManager.GetTokenClaims(token, model.Certificate).ToList();
                    var email = claims.FirstOrDefault(item => item.Type.Equals(ClaimTypes.Email)).Value;
                    var organisations = claims.Where(item => item.Type.Equals(ClaimTypes.Name));

                    List<string> orgs = new List<string>
                    {
                        email
                    };

                    foreach (var item in organisations)
                    {
                        orgs.Add(item.Value);
                    }
                }
            }
             return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}