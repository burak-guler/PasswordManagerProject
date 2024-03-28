namespace PasswordManager.Core.Models
{
    public class LoginResponse
    {
        public bool AuthenticateResult { get; set; }
        public string AuthToken { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public int LevelID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LevelName { get; set; }
        public string CompanyName { get; set; }
        public DateTime AccessTokenExpireDate { get; set; }

    }
}
