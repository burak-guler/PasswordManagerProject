namespace PasswordManager.WebApp.Models
{
    public class PasswordResponse
    {
        public int PasswordID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string PasswordValue { get; set; }
    }
}
