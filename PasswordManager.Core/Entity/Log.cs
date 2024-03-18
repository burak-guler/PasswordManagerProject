using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Core.Entity
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        public int CompanyID { get; set; }
        public DateTime LogDate { get; set; }
        public string LogDetail { get; set; }
    }
}
