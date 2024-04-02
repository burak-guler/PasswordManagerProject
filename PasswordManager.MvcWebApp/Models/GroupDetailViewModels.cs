using PasswordManager.Core.Models;

namespace PasswordManager.MvcWebApp.Models
{
    public class GroupDetailViewModels
    {
        public GroupViewModels groupViewModels { get; set; } = new GroupViewModels();
        public List<UserViewModels> userViewsModels { get; set; } = new List<UserViewModels>();
        public List<RoleViewsModels> roleViewModels { get; set; } = new List<RoleViewsModels>();
    }
}
