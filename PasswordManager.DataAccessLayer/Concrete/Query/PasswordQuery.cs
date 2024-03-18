using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class PasswordQuery
    {
        public static readonly string ADD = @"INSERT INTO dbo.Tbl_Password VALUES (@UserID, @CategoryID, @PasswordValue,@LevelID,@CompanyID,@IsActive)";

        public static readonly string GET = @"SELECT * FROM dbo.Tbl_Password WHERE PasswordID=@id";


        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_Password where IsActive=1";

        public static readonly string GET_LIST_COMPANYID = @"SELECT * FROM dbo.Tbl_Password 
                                                            where IsActive=1 and CompanyID=@companyId";

        public static readonly string REMOVE = @"Update dbo.Tbl_Password 
                                                set IsActive=0 
                                                WHERE PasswordID = @id";

        public static readonly string UPDATE = @"UPDATE dbo.Tbl_Password SET UserID=@UserID, CategoryID=@CategoryID, PasswordValue=@PasswordValue, LevelID=@LevelID, CompanyID=@CompanyID WHERE PasswordID=@PasswordID";

        //PasswordAccess Query

        public static readonly string PasswordAccesADD = @"
                                        INSERT INTO dbo.Lkp_PasswordAcces 
                                        VALUES (@PasswordID, @UserID,@RoleID)";
    }
}
