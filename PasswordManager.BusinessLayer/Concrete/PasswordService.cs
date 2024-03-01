using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordService(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task< Password> GetPassword(int id)
        {
            return await _passwordRepository.Get(id);
        }

        public async Task< List<Password>> GetPasswordList()
        {
           return await _passwordRepository.List();
        }

        public async Task PasswordAdd(Password password)
        {
           await _passwordRepository.Add(password);
        }

        public async Task PasswordRemove(int id)
        {
           await _passwordRepository.Remove(id);
        }

        public async Task PasswordUpdate(Password password)
        {
            await _passwordRepository.Update(password);
        }
    }
}
