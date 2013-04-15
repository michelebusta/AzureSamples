
using SocialLoginForms.DataAccess;
using SocialLoginForms.DataAccess.Sql;
using SocialLoginForms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialLoginForms.Security
{
    public class ProfileAuthenticationManager : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            var nameIdClaim = incomingPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
      
            bool hasProfile = false;

            // check if we have profile for user, if not redirect to Profile page
            // if we do, create a new claims principal to return and add new claims to it
            // new claims will be name claim, email claim
            IUserDataAccess userDal = new UserDataAccess();
            hasProfile = userDal.CheckUserExists(nameIdClaim.Value);

            if (hasProfile)
            {
                // add the profile claims to the principal and return them
                ClaimsPrincipal cp = new ClaimsPrincipal();
                ClaimsIdentity identity = new ClaimsIdentity("SocialLogin");
                foreach (Claim c in incomingPrincipal.Claims)
                {
                    identity.AddClaim(c.Clone());
                }

                var profile = userDal.GetProfile(nameIdClaim.Value);

                HttpContext.Current.Response.Cookies.Remove("ProfileId");
                HttpCookie cookie = new HttpCookie("ProfileId", profile.Id.ToString());
                cookie.Expires = DateTime.MaxValue; // make it infinite, we'll remove when you complete confirmation and account setup
                HttpContext.Current.Response.AppendCookie(cookie);

                identity.AddClaim(new Claim("ProfileId", profile.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, profile.FriendlyName));
                identity.AddClaim(new Claim(ClaimTypes.Email, profile.Email));

                cp.AddIdentity(identity);

                return cp;
            }
            else
            {
                
                // user gets here by associating new social account while logged in
                //var profileIdClaim = incomingPrincipal.Claims.FirstOrDefault(x => x.Type == "ProfileId");
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ProfileId"];

                // get profile back
                Guid id = new Guid(cookie.Value);
                var profile = userDal.GetProfile(id);

                // associate account to profile
                var issuerClaim = incomingPrincipal.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider");

                // TODO: improve how issuer is stored, enum isn't appropriate
                // had to hack to get socialsts in there
                string issuer = issuerClaim.Value.Replace("uri:", "");
                if (issuer.ToLower().Contains("socialsts.azurewebsites.net/demo/twitter"))
                    issuer = "Twitter";
                    //https://socialsts.azurewebsites.net/demo/twitter/authenticate/
                userDal.AssociateSocialAccountToProfile(nameIdClaim.Value, (Issuer)Enum.Parse(typeof(Issuer), issuer), profile.Id);
                
                ClaimsPrincipal cp = new ClaimsPrincipal();
                ClaimsIdentity identity = new ClaimsIdentity("SocialLogin");
                foreach (Claim c in incomingPrincipal.Claims)
                {
                    identity.AddClaim(c.Clone());
                }

                identity.AddClaim(new Claim(ClaimTypes.Name, profile.FriendlyName));
                identity.AddClaim(new Claim(ClaimTypes.Email, profile.Email));

                cp.AddIdentity(identity);

                return cp;
                
                // let session module do its job
                // writes cookie with nameid, issuer and we redirect to profile page

            }

        }

    }
}
