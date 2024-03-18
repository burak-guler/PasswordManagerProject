using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class GroupQuery
    {

        public static readonly string ADD = @"
                        INSERT INTO dbo.Tbl_Group (CreationDate,CompanyID) 
                        VALUES (@CreationDate,@CompanyID);
                        INSERT INTO dbo.Lang_Group (GroupID, LangID, GroupName,GroupDescription)
                        VALUES (@@identity, @LangID, @GroupName,@GroupDescription);";
        

        public static readonly string GET = @"
                        select tbl.GroupID,tbl.CompanyID, lang.GroupName,lang.GroupDescription,tbl.CreationDate, lang.LangID from Tbl_Group as tbl 
                        inner join Lang_Group as lang 
                        on tbl.GroupID=lang.GroupID 
                        where tbl.GroupID=@id";

        public static readonly string GET_LIST = @"
                                                select tbl.GroupID,tbl.CompanyID,lang.GroupName,lang.GroupDescription,
                                                tbl.CreationDate,lang.LangID  
                                                from Tbl_Group as tbl 
                                                inner join Lang_Group as lang 
                                                on tbl.GroupID=lang.GroupID";

        public static readonly string GET_LIST_COMPANYID = @"
                                                select tbl.GroupID,tbl.CompanyID,lang.GroupName,lang.GroupDescription,
                                                tbl.CreationDate,lang.LangID  
                                                from Tbl_Group as tbl 
                                                inner join Lang_Group as lang 
                                                on tbl.GroupID=lang.GroupID
                                                where CompanyID=@CompanyID";

        public static readonly string REMOVE = @"DELETE FROM dbo.Lang_Group 
                                                WHERE GroupID = @id
                                                
                                                DELETE FROM dbo.Tbl_Group 
                                                WHERE GroupID = @id";

        public static readonly string UPDATE = @"
                update Tbl_Group set CreationDate=@CreationDate, CompanyID=@CompanyID
                where GroupID=@GroupID 
                update Lang_Group  set GroupName=@GroupName, GroupDescription=@GroupDescription
                where GroupID=@GroupID and LangID=@LangID ";

        //GroupRole Query
        public static readonly string GroupRoleADD = @"
                                        INSERT INTO dbo.Lkp_GroupRole 
                                        VALUES (@GroupID, @RoleID)";

        //UserGroup Query

        public static readonly string UserGroupADD = @"
                                        INSERT INTO dbo.Lkp_UserGroup 
                                        VALUES (@UserID, @GroupID)";
    }
}
