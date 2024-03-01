using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
    public class Password
    {
        [Key]
        public int PasswordID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string PasswordValue { get; set; }
    }
}
