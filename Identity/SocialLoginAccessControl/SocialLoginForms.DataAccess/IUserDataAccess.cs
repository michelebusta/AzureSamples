using SocialLoginForms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialLoginForms.DataAccess
{
    public interface IUserDataAccess
    {
        /// <summary>
        /// Check if user exists
        /// </summary>
        /// <param name="login">User name or name identifier</param>
        /// <returns>True if user exists for login (name or identifier)</returns>
        bool CheckUserExists(string login);

        /// <summary>
        /// Create user, userlogin, and profile for user
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="friendlyName">Friendly name for user</param>
        void CreateUserAccount(string userName, string password, string email, string friendlyName);

        /// <summary>
        /// Create user, userlogin, and profile for social user
        /// </summary>
        /// <param name="nameId">Name identifier from ACS</param>
        /// <param name="issuer">Issuer of identifier</param>
        /// <param name="email">Email address</param>
        /// <param name="friendlyName">Friendly name for user</param>
        void CreateUserAccount(string nameId, Issuer issuer, string email, string friendlyName);


        /// <summary>
        /// Get profile for nameId
        /// </summary>
        /// <param name="nameId">Name identifier</param>
        /// <returns>Profile</returns>
        Profile GetProfile(string nameId);

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Profile AuthenticateUser(string userName, string password);

        /// <summary>
        /// Associate social account to existing profiel
        /// </summary>
        /// <param name="nameId"></param>
        /// <param name="issuer"></param>
        /// <param name="profileId"></param>
        void AssociateSocialAccountToProfile(string nameId, Issuer issuer, Guid profileId);

        Entities.Profile GetProfile(Guid od);
    }
}
