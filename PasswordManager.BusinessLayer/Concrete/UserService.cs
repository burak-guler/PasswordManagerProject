using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUser(int id)
        {
           return await _userRepository.Get(id);
        }

        public async Task< List<User>> GetUserList()
        {
           return await _userRepository.List();
        }

        public async Task<User> Login(User user)
        {
           return await _userRepository.Login(user);
        }        

        public async Task UserAdd(User user)
        {
            await _userRepository.Add(user);
        }

        public async Task UserRemove(int id)
        {
           await _userRepository.Remove(id);
        }

        public async Task UserUpdate(User user)
        {
          await  _userRepository.Update(user);
        }
    }
}
