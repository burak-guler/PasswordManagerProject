using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
    public class Role
    {
        public enum UserRole
        {
            Admin = 1,
            Moderator = 2,
            User = 3,
            PasswordAcces = 4,
        }
    }
}
