using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class UserQuery
    {
        public static readonly string ADD = @"
                                        INSERT INTO dbo.Tbl_Users 
                                        VALUES (@UserName, @Password,@CompanyID,@LevelID,@CreationDate,@IsActive)";

        public static readonly string GET = @"SELECT * FROM dbo.Tbl_Users WHERE UserID=@id";

        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_Users where IsActive=1";

        public static readonly string GET_LIST_COMPANYID = @"SELECT * FROM dbo.Tbl_Users 
                                                           where IsActive=1 and CompanyID=@companyId";


        public static readonly string REMOVE = @"update dbo.Tbl_Users set IsActive=0 WHERE UserID = @id";        

        public static readonly string UPDATE = @"
                                    UPDATE dbo.Tbl_Users 
                                    SET UserName = @UserName,Password = @Password,CompanyID = @CompanyID,LevelID = @LevelID, CreationDate = @CreationDate WHERE UserID =@UserID";

        public static readonly string LOGIN = @"select * from Tbl_Users where UserName = @UserName and Password = @Password";

        //UserRole Queery
        public static readonly string UserRoleADD = @"
                                        INSERT INTO dbo.Lkp_UserRole 
                                        VALUES (@UserID, @RoleID)";

    }
}
