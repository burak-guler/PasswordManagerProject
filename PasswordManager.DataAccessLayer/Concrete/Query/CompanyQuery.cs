using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class CompanyQuery
    {
        public static readonly string ADD = @"
                                            DECLARE @CompanyId INT;

                                            INSERT INTO dbo.Tbl_Company (CompanyName, IsActive) 
                                            VALUES (@CompanyName, @IsActive);
                                            SET @CompanyId = SCOPE_IDENTITY();

                                            DECLARE @UserLevelId INT;
                                            INSERT INTO dbo.Tbl_UserLevel (CreationDate, IsActive, CompanyID) 
                                            VALUES (@CreationDate, @IsActive, @CompanyId);
                                            SET @UserLevelId = SCOPE_IDENTITY();

                                            INSERT INTO dbo.Lang_UserLevel (LevelID, LangID, LevelName)
                                            VALUES (@UserLevelId, @LangID,@LevelName);";

        public static readonly string GET = @"
                                            SELECT * FROM dbo.Tbl_Company 
                                            WHERE CompanyID=@id";

        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_Company where IsActive=1 ";

        public static readonly string REMOVE = @"
                                                Update dbo.Tbl_Company 
                                                set IsActive=0 
                                                WHERE CompanyID = @id";

        public static readonly string UPDATE = @"
                                                UPDATE dbo.Tbl_Company 
                                                SET CompanyName = @CompanyName 
                                                WHERE CompanyID =@CompanyID";
    }
}
