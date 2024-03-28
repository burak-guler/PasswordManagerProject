namespace PasswordManager.Core.Models
{
    public class UserViewModels
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CompanyID { get; set; }
        public int LevelID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public string? LevelName { get; set; }
        public string? CompanyName { get; set; }

    }
}
