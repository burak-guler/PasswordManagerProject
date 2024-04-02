using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Models
{
    public class PasswordViewModels
    {
        public int PasswordID { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public int CategoryID { get; set; }
        public int? RoleID { get; set; }
        public string? CategoryName { get; set; }
        public string? PasswordValue { get; set; }
        public string? LevelName { get; set; }
        public int LevelID { get; set; }
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public bool IsActive { get; set; }
    }
}
