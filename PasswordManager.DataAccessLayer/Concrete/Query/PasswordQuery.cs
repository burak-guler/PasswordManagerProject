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

        public static readonly string GET = @"SELECT P.PasswordID,
                                                           U.UserName,
	                                                       P.UserID,
                                                           LC.CategoryName ,
	                                                       P.CategoryID,
                                                           LUL.LevelName,
	                                                       P.LevelID,
                                                           P.PasswordValue,
                                                           C.CompanyName,
	                                                       P.CompanyID,
                                                           P.IsActive
                                                        FROM dbo.Tbl_Password P
                                                        JOIN dbo.Tbl_Users U ON P.UserID = U.UserID
                                                        JOIN dbo.Lang_Category LC ON P.CategoryID = LC.CategoryID
                                                        JOIN dbo.Lang_UserLevel LUL ON P.LevelID = LUL.LevelID
                                                        JOIN dbo.Tbl_Company C ON P.CompanyID = C.CompanyID
                                                        WHERE P.IsActive = 1 AND P.PasswordID = @PasswordID;";


        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_Password where IsActive=1";

        public static readonly string GET_LIST_USERID = @"SELECT P.PasswordID,
                                                           U.UserName,
	                                                       P.UserID,
                                                           LC.CategoryName ,
	                                                       P.CategoryID,
                                                           LUL.LevelName,
	                                                       P.LevelID,
                                                           P.PasswordValue,
                                                           C.CompanyName,
	                                                       P.CompanyID,
                                                           P.IsActive
                                                        FROM dbo.Tbl_Password P
                                                        JOIN dbo.Tbl_Users U ON P.UserID = U.UserID
                                                        JOIN dbo.Lang_Category LC ON P.CategoryID = LC.CategoryID
                                                        JOIN dbo.Lang_UserLevel LUL ON P.LevelID = LUL.LevelID
                                                        JOIN dbo.Tbl_Company C ON P.CompanyID = C.CompanyID
                                                        WHERE P.IsActive = 1 AND U.UserID = @UserID;";

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

        public static readonly string PasswordAccesRemove = @"
                                        Delete From dbo.Lkp_PasswordAcces 
                                        where  PasswordID=@PasswordID and  UserID=@UserID and RoleID=@RoleID";

        public static readonly string PASSWORDROLE_CHECK = @"SELECT tbl.* FROM dbo.Tbl_Password tbl
                                                             inner join dbo.Lkp_PasswordAcces lkp 
                                                             ON  tbl.PasswordID=lkp.PasswordID
                                                             where lkp.PasswordID=@PasswordID and lkp.UserID=@UserID and                                                     lkp.RoleID=@RoleID";

        public static readonly string PasswordAccesGetList = @"
                                                            SELECT 
                                                                p.PasswordID,
                                                                pa.UserID,
                                                                p.CategoryID,
                                                                p.PasswordValue,
                                                                p.LevelID,
                                                                p.CompanyID,
                                                                p.IsActive,
                                                                lv.LevelName,
                                                                ca.CategoryName,
                                                                c.CompanyName,
                                                                pa.RoleID
                                                            FROM Lkp_PasswordAcces pa
                                                            INNER JOIN Tbl_Users u ON pa.UserID = u.UserID
                                                            INNER JOIN Tbl_Password p ON p.PasswordID = pa.PasswordID
                                                            INNER JOIN Lang_UserLevel lv ON lv.LevelID = p.LevelID
                                                            INNER JOIN Lang_Category ca ON ca.CategoryID = p.CategoryID
                                                            INNER JOIN Tbl_Company c ON c.CompanyID = p.CompanyID
                                                            WHERE u.UserID = @UserID and pa.RoleID= @RoleID;";

    }
}
