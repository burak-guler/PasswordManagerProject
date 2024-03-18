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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task Add(Company entity)
        {
           await _companyRepository.Add(entity); 
        }

        public async Task<List<Company>> GetAll()
        {
           return await  _companyRepository.List();
        }

        public async Task<Company> GetById(int id)
        {
           return await _companyRepository.Get(id);
        }

        public async Task Remove(int id)
        {
          await  _companyRepository.Remove(id);
        }

        public async Task Update(Company entity)
        {
            await _companyRepository.Update(entity);
        }
    }
}
