using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using System.ComponentModel.Design;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class UserService : BaseService<UserViewModels>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserLevelRepository _userLevelRepository;

        public UserService(IUserRepository userRepository, IUserLevelRepository userLevelRepository)
        {
            _userRepository = userRepository;
            _userLevelRepository = userLevelRepository;
        }

        public async Task Add(UserViewModels entity, int? id)
        {
            var user = await _userRepository.Get(id.Value);
            var level = await _userLevelRepository.Get(user.LevelID);
            if (user != null)
            {
                if (level.LevelName == "Admin")
                {
                    var userCheck = await _userRepository.UserCheck(entity);
                    if (userCheck == null)
                    {
                        await _userRepository.Add(entity);
                    }
                    else { throw new InvalidOperationException("Kullanıcı Adını değiştirin."); }
                }
                else { throw new UnauthorizedAccessException("Kullanıcı yetki dışı."); }
            }
            else { throw new UnauthorizedAccessException("Kullanıcı bulunamadı"); }                     

        }

        public async Task AddUserToRole(int userID, int roleID)
        {
            await _userRepository.AddUserToRole(userID, roleID);
        }

        public async Task<List<UserViewModels>> GetAll()
        {
            return await _userRepository.List();
        }

        public async Task<List<UserViewModels>> GetAllByCompanyId(int companyId)
        {
            return await _userRepository.GetAllByCompanyId(companyId);
        }

        public async Task<List<UserRoleViewModels>> GetAllUserRoleByUserID(int userID)
        {
            return await _userRepository.GetAllUserRoleByUserID(userID);
        }

        public async Task<UserViewModels> GetById(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<UserViewModels> Login(UserViewModels user)
        {
           return await _userRepository.Login(user);
        }

        public async Task Remove(int id)
        {
            await _userRepository.Remove(id);

        }

        public async Task RemoveUserToRole(int userRoleID)
        {
            await _userRepository.RemoveUserToRole(userRoleID);
        }

        public async Task Update(UserViewModels entity)
        {
            await _userRepository.Update(entity);

        }
        
    }
}
