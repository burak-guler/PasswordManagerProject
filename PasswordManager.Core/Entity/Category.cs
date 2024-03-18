using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace PasswordManager.Core.Entity
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public int CompanyID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

        // Lang_Category tablosundaki CategoryName alanı
        public string CategoryName { get; set; }
        public int LangID { get; set; }
    }
}
