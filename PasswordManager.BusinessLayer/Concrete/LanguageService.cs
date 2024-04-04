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
    public class LanguageService :BaseService<Language>, ILanguageService
    {
        private ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public Task Add(Language entity, int? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Language>> GetAll()
        {
            return _languageRepository.List();
        }

        public Task<Language> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Language entity)
        {
            throw new NotImplementedException();
        }
    }
}
