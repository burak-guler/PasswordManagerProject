using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }

    }
}
