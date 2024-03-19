using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class UserLevelQuery
    {
        public static readonly string ADD = @"
                                                INSERT INTO dbo.Tbl_UserLevel (CreationDate, IsActive, CompanyID) 
                                                VALUES (@CreationDate,@IsActive,@CompanyID);
                                                INSERT INTO dbo.Lang_UserLevel (LevelID, LangID, LevelName)
                                                VALUES (@@identity, @LangID, @LevelName);";

        public static readonly string GET = @"
                                                select lang.LevelName,lang.LangID,tbl.IsActive,tbl.LevelID, tbl.CompanyID,                              tbl.CreationDate 
                                                from Tbl_UserLevel as tbl 
                                                inner join Lang_UserLevel as lang 
                                                on tbl.LevelID=lang.LevelID 
                                                where tbl.LevelID=@id and tbl.IsActive=1";

        public static readonly string GET_LIST = @"
                                                select lang.LevelName,lang.LangID,                                                         tbl.CompanyID,tbl.IsActive,tbl.LevelID,tbl.CreationDate 
                                                from Tbl_UserLevel as tbl 
                                                inner join Lang_UserLevel as lang 
                                                on tbl.LevelID=lang.LevelID  
                                                where tbl.IsActive=1";

        public static readonly string GET_LIST_COMPANYID = @"
                                                select lang.LevelName,lang.LangID,                                                        tbl.CompanyID,tbl.IsActive,tbl.LevelID,tbl.CreationDate 
                                                from Tbl_UserLevel as tbl 
                                                inner join Lang_UserLevel as lang 
                                                on tbl.LevelID=lang.LevelID  
                                                where tbl.IsActive=1 and tbl.CompanyID=@companyId";

        public static readonly string REMOVE = "update Tbl_UserLevel set IsActive=0 where LevelID=@id";

        public static readonly string UPDATE = @"
                                                update Tbl_UserLevel set CreationDate=@CreationDate , CompanyID=@CompanyID 
                                                where LevelID=@LevelID 
                                                update Lang_UserLevel set LevelName=@LevelName 
                                                where LevelID=@LevelID and LangID=@LangID";
    }
}
