using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class CategoryQuery
    {
        public static readonly string ADD = "INSERT INTO dbo.Tbl_Category VALUES (@CategoryName)";
        public static readonly string GET = "SELECT * FROM dbo.Tbl_Category WHERE CategoryID=@id";
        public static readonly string GET_LIST = "SELECT * FROM dbo.Tbl_Category";
        public static readonly string REMOVE = "DELETE FROM dbo.Tbl_Category WHERE CategoryID = @id";
        public static readonly string UPDATE = "UPDATE dbo.Tbl_Category SET CategoryName = @CategoryName WHERE CategoryID =@CategoryID";
    }
}
