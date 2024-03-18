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
    public class LogService : ILogService
    {
        private ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task Add(Log entity)
        {
            await _logRepository.Add(entity);   
        }

        public async Task<List<Log>> GetAll()
        {
            return await _logRepository.List();
        }

        public async Task<List<Log>> GetAllByCompanyId(int companyId)
        {
            return await _logRepository.GetAllByCompanyId(companyId);
        }

        public async Task<Log> GetById(int id)
        {
            return await _logRepository.Get(id);  
        }

        public async Task Remove(int id)
        {
            await _logRepository.Remove(id);    
        }

        public async Task Update(Log entity)
        {
            await _logRepository.Update(entity);
        }
    }
}
