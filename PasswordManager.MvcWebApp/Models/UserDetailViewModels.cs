using PasswordManager.Core.Models;

namespace PasswordManager.MvcWebApp.Models
{
    public class UserDetailViewModels
    {
        public UserViewModels userViewModels {  get; set; }=new UserViewModels();
         public List<GroupViewModels> groupViewModels { get; set; } = new List<GroupViewModels>();
         public List<RoleViewsModels> roleViewModels { get; set; } = new List<RoleViewsModels>();
       
    }
}
