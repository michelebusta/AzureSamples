using SocialLoginForms.DataAccess;
using SocialLoginForms.DataAccess.Sql;
using SocialLoginModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SocialLoginForms.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            var cp = System.Threading.Thread.CurrentPrincipal;

            var identity = cp.Identity as ClaimsIdentity;
            var nameIdClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            ProfileModel profileModel = new ProfileModel();

            if (nameIdClaim != null)
            {
                IUserDataAccess userDal = new UserDataAccess();
                var profile = userDal.GetProfile(nameIdClaim.Value);
             
                profileModel.Email = profile.Email;
                profileModel.FriendlyName = profile.FriendlyName;
                profileModel.NameId = profile.UserLogins.First().NameID;
                profileModel.Issuer = profile.UserLogins.First().Issuer.ToString();
            }

            return View(profileModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
