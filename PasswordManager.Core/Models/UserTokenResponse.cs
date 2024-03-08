using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Models
{
    public class UserTokenResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
        public string AuthToken { get; set; }
        
    }
}
