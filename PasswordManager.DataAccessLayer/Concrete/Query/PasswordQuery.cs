using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class PasswordQuery
    {
        public static readonly string ADD = "INSERT INTO dbo.Tbl_Password VALUES (@UserID, @CategoryID, @PasswordValue)";
        public static readonly string GET = "SELECT * FROM dbo.Tbl_Password WHERE PasswordID=@id";
        public static readonly string GET_LIST = "SELECT * FROM dbo.Tbl_Password";
        public static readonly string REMOVE = "DELETE FROM dbo.Tbl_Password WHERE PasswordID = @id";
        public static readonly string UPDATE = "UPDATE dbo.Tbl_Password SET UserID=@UserID, CategoryID=@CategoryID, PasswordValue=@PasswordValue WHERE PasswordID=@PasswordID";
    }
}
