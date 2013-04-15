using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialLoginForms.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string FriendlyName { get; set; }
        public string Email { get; set; }

        public IEnumerable<UserLogin> UserLogins { get; set; }
    }
}
