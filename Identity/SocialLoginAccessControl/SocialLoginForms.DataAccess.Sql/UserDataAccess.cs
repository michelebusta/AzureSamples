using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialLoginForms.Entities;

namespace SocialLoginForms.DataAccess.Sql
{

    public class UserDataAccess : IUserDataAccess
    {
        private SocialLoginEntities _entities = null;

        public UserDataAccess()
        {
            _entities = new SocialLoginEntities();

            if (!_entities.DatabaseExists())
                _entities.CreateDatabase();
        }

        private User CreateUserAndProfile(string email, string friendlyName)
        {
            Profile profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Name = friendlyName,
            };
            _entities.Profiles.AddObject(profile);

            User user = new User()
            {
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ProfileId = profile.Id,
            };
            _entities.Users.AddObject(user);

            return user;
        }


        public bool CheckUserExists(string login)
        {
            int countForLogin = _entities.UserLogins.Where(z => z.Login.Equals(login, StringComparison.CurrentCultureIgnoreCase)).Count();

            return countForLogin == 0 ? false : true;
        }

        public void CreateUserAccount(string userName, string password, string email, string friendlyName)
        {
            // throw if userName aleady exists
            var count = _entities.UserLogins.Where(e => e.Login.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).Count();
            if (count > 0)
                throw new ArgumentException("NameId already exists", "NameId");

            User user = CreateUserAndProfile(email, friendlyName);

            UserLogin userLogin = new UserLogin()
            {
                Id = Guid.NewGuid(),
                Issuer = (int)Issuer.Forms,
                Password = password,
                Login = userName,
                UserId = user.Id, 
            };
            _entities.UserLogins.AddObject(userLogin);

            _entities.SaveChanges();
        }

        public void CreateUserAccount(string nameId, Issuer issuer, string email, string friendlyName)
        {
            // throw if nameId already exists
            var count = _entities.UserLogins.Where(e => e.Login.Equals(nameId, StringComparison.CurrentCultureIgnoreCase)).Count();
            if (count > 0)
                throw new ArgumentException("NameId already exists", "NameId");

            User user = CreateUserAndProfile(email, friendlyName);

            UserLogin userLogin = new UserLogin()
            {
                Id = Guid.NewGuid(),
                Issuer = (int)issuer,
                Password = null,
                Login = nameId,
                UserId = user.Id,
            };
            _entities.UserLogins.AddObject(userLogin);

            _entities.SaveChanges();
        }

        public Entities.Profile GetProfile(Guid od)
        {
            var profile = _entities.Profiles.Where(e => e.Id.Equals(od)).First();

            Entities.Profile profileEntity = new Entities.Profile()
            {
                FriendlyName = profile.Name,
                Email = profile.Email,
                Id = profile.Id,
            };

            // load user logins for profile
            profileEntity.UserLogins = profile.Users.SelectMany(e => e.UserLogins).
                Select(e => new Entities.UserLogin()
                {
                    Issuer = (Issuer)e.Issuer,
                    NameID = e.Login,
                }).ToList();

            return profileEntity;
        }

        public Entities.Profile GetProfile(string nameId)
        {
            var userLogin = _entities.UserLogins.Where(e => e.Login.Equals(nameId, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (userLogin == null)
                return null;

            var profile = userLogin.User.Profile;

            Entities.Profile profileEntity = new Entities.Profile()
            {
                FriendlyName = profile.Name,
                Email = profile.Email,
                Id = profile.Id,
            };

            // load user logins for profile
            profileEntity.UserLogins = profile.Users.SelectMany(e => e.UserLogins).
                Select(e => new Entities.UserLogin()
                {
                    Issuer = (Issuer)e.Issuer,
                    NameID = e.Login,
                }).ToList();

            return profileEntity;
        }

        /// <returns></returns>
        public Entities.Profile AuthenticateUser(string userName, string password)
        {
            var userLogin = _entities.UserLogins.Where(e => e.Login.Equals(userName, StringComparison.CurrentCultureIgnoreCase) &&
                e.Password.Equals(password)).FirstOrDefault();

            if (userLogin == null)
                return null;

            var prof = userLogin.User.Profile;

            return new Entities.Profile()
            {
                FriendlyName = prof.Name,
                Email = prof.Email,
                Id = prof.Id
            };
        }


        public void AssociateSocialAccountToProfile(string nameId, Issuer issuer, Guid profileId)
        {
            // get the profile/user we need to add to
            var profile = _entities.Profiles.Where(e => e.Id.Equals(profileId)).FirstOrDefault();

            if (profile == null)
            {
                throw new InvalidOperationException("Profile doesn't exist");
            }
            
            var userLogin = new UserLogin()
            {
                Id = Guid.NewGuid(),
                Issuer = (int)issuer,
                Login = nameId,
                UserId = profile.Users.First().Id,
            };

            // add userLogin to profile/user
            _entities.UserLogins.AddObject(userLogin);

            _entities.SaveChanges();
        }
    }
}
