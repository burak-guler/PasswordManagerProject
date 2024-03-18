using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyID { get; set; }

        // Lang_Group tablosundaki Fieldlar
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int  LangID { get; set; }
    }
}
