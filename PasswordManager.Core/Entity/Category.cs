using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Core.Entity
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
