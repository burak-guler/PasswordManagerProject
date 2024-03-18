using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
   
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CompanyID { get; set; }
        public int LevelID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
