using Microsoft.Extensions.Configuration;
using PasswordManager.DataAccessLayer.Abstract;
using System.Data.SqlClient;

namespace PasswordManager.DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        

        private readonly IConfiguration configuration;
        public GenericRepository(IConfiguration configuration)
        {
            this.configuration = configuration;       }

        
        public async Task<SqlConnection> ConnectionDb ()
        {
            
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();
            return connection;
        }

        public Task Add(T value)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> List()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T value)
        {
            throw new NotImplementedException();
        }
    }
}
