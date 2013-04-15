using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;

namespace SocialLoginForms
{
    public class MVCFederationModule : WSFederationAuthenticationModule
    {

        protected override void OnAuthenticateRequest(object sender, EventArgs args)
        {
             base.OnAuthenticateRequest(sender, args);
        }
    }
}