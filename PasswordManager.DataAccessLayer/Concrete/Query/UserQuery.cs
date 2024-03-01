using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class UserQuery
    {
        public static readonly string ADD = "INSERT INTO dbo.Tbl_Users VALUES (@UserName, @Password)";
        public static readonly string GET = "SELECT * FROM dbo.Tbl_Users WHERE UserID=@id";
        public static readonly string GET_LIST = "SELECT * FROM dbo.Tbl_Users";
        public static readonly string REMOVE = "DELETE FROM dbo.Tbl_Users WHERE UserID = @id";
        public static readonly string UPDATE = "UPDATE dbo.Tbl_Users SET UserName = @UserName,Password = @Password WHERE UserID =@UserID";
        public static readonly string LOGIN = "select * from Tbl_Users where UserName = @UserName and Password = @Password";
    }
}
