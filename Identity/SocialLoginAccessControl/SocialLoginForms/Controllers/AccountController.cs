using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SocialLoginForms.Filters;
using SocialLoginForms.Models;
using SocialLoginForms.DataAccess.Sql;
using System.Security.Claims;
using SocialLoginModules.Models;
using SocialLoginForms.Entities;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using SocialLoginForms.DataAccess;

namespace SocialLoginForms.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/CreateSocial
        //[Authorize]
        public ActionResult CreateSocial()
        {
            // TODO:
            // we have cookie with name id, issuer
            // we are assuming if you got to this page you don't have name/email later we can check this
            // we assume you can't get here unless you have a cookie
            ClaimsIdentity identity = System.Threading.Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            var nameIdClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var issuerClaim = identity.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider");

            var issuer = issuerClaim.Value.Replace("uri:", "");

            if (issuer.ToLower().Contains("facebook"))
            {
                issuer = "Facebook";
            }

            ProfileModel profileModel = new ProfileModel { Issuer = issuer, NameId = nameIdClaim.Value };
            return View(profileModel);
        }

        // POST: /Account/Manage
        [HttpPost]
        public ActionResult CreateSocial(ProfileModel model)
        {

            UserDataAccess userDataAccess = new UserDataAccess();
            userDataAccess.CreateUserAccount(model.NameId, (Issuer)Enum.Parse(typeof(Issuer), model.Issuer, true), model.Email, model.FriendlyName);

            // TODO: update the cookie to include name/email
            // create a new claims principal 
            // add email, name claim
            // write it then redirect
            // check that we get the full set of claims in the authorize override of global

            ClaimsIdentity oldIdentity = System.Threading.Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            ClaimsPrincipal cp = new ClaimsPrincipal();
            ClaimsIdentity newIdentity = new ClaimsIdentity("SocialLogin");
            foreach (Claim c in oldIdentity.Claims)
            {
                newIdentity.AddClaim(c.Clone());
            }
            newIdentity.AddClaim(new Claim(ClaimTypes.Name, model.FriendlyName));
            newIdentity.AddClaim(new Claim(ClaimTypes.Email, model.Email));
            cp.AddIdentity(newIdentity);

            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(new SessionSecurityToken(cp));


            return RedirectToAction("Index", "Home");
        }

        private bool InternalLogin(string userName, string password)
        {
            // valdiate login
            IUserDataAccess userDal = new UserDataAccess();
            var profile = userDal.AuthenticateUser(userName, password);

            if (profile == null)
            {
                return false;
            }

            ClaimsPrincipal cp = new ClaimsPrincipal();
            ClaimsIdentity newIdentity = new ClaimsIdentity("SocialLogin");

            newIdentity.AddClaim(new Claim("ProfileId", profile.Id.ToString()));
            newIdentity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", Issuer.Forms.ToString()));
            newIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userName));
            newIdentity.AddClaim(new Claim(ClaimTypes.Name, profile.FriendlyName));
            newIdentity.AddClaim(new Claim(ClaimTypes.Email, profile.Email));
            cp.AddIdentity(newIdentity);

            
            Response.Cookies.Remove("ProfileId");
            HttpCookie cookie = new HttpCookie("ProfileId", profile.Id.ToString());
            cookie.Expires = DateTime.MaxValue; // make it infinite, we'll remoeve when you complete confirmation and account setup
           Response.AppendCookie(cookie);

            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(new SessionSecurityToken(cp));

            return true;
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!InternalLogin(model.UserName, model.Password))
            {
                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                UserDataAccess userDataAccess = new UserDataAccess();
                userDataAccess.CreateUserAccount(model.UserName, model.Password, model.Email, model.DisplayName);

                if (!InternalLogin(model.UserName, model.Password))
                {
                    // If we got this far, something failed, redisplay form
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult LogOut()
        {
            FederatedAuthentication.SessionAuthenticationModule.DeleteSessionTokenCookie();

            return RedirectToAction("Login", "Account");
        }




    }
}
