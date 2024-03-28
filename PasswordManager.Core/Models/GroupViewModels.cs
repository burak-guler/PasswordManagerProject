using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Models
{
    public class GroupViewModels
    {
        public int GroupID { get; set; }
        public int CompanyID { get; set; }
        public int GroupLangID { get; set; }
        public int LangID { get; set; }
        public int UserGroupID { get; set; }
        public string CreationDate { get; set; }
        public string GroupDescription { get; set; }
        public string GroupName { get; set; }
        public string CompanyName { get; set; }
        public string LangName { get; set; }
    }
}
