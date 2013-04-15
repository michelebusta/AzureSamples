using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SocialLoginForms
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            var cp = System.Threading.Thread.CurrentPrincipal;

            var identity = cp.Identity as ClaimsIdentity;

            if (identity.HasClaim(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider"))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["ProfileId"];
                if (cookie == null)
                {
                    if (!Request.Url.AbsoluteUri.Contains("Account/CreateSocial"))
                    {
                        Response.Redirect("~/Account/CreateSocial", true);
                    }
                }
            }
    


            //if (!identity.HasClaim(x => x.Type == System.IdentityModel.Claims.ClaimTypes.Name))
            //{
            //    if (!Request.Url.AbsoluteUri.Contains("Account/CreateSocial"))
            //    {
            //        Response.Redirect("~/Account/CreateSocial", true);
            //    }
            //}

           
        }

     

          
    }
}