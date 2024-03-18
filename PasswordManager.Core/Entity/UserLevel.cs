using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
    public class UserLevel
    {
        [Key]
        public int LevelID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public int CompanyID { get; set; }

        // Lang_UserLevel tablosundaki Field alanı
        public string LevelName { get; set; }
        public int LangID { get; set; }
    }
}
