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
                        OUTPUT INSERTED.GroupID
                        VALUES (@CreationDate,@CompanyID);";

        public static readonly string LANG_ADD = @"
                        INSERT INTO dbo.Lang_Group (GroupID, LangID, GroupName,GroupDescription)
                        VALUES (@@identity, @LangID, @GroupName,@GroupDescription);";


        public static readonly string GET = @"
                                            SELECT tbl.GroupID, tbl.CompanyID, lang.GroupName, lang.GroupDescription, tbl.CreationDate,          lang.LangID,c.CompanyName, tl.LangName,lang.GroupLangID
                                            FROM Tbl_Group AS tbl
                                            INNER JOIN Lang_Group AS lang ON tbl.GroupID = lang.GroupID
                                            INNER JOIN Tbl_Company AS c ON tbl.CompanyID = c.CompanyID
                                            INNER JOIN Tbl_Language AS tl ON lang.LangID = tl.LangID
                                            WHERE tbl.GroupID = @GroupID;";

        public static readonly string GET_LIST = @"
                                                select tbl.GroupID,tbl.CompanyID,lang.GroupName,lang.GroupDescription,
                                                tbl.CreationDate,lang.LangID  
                                                from Tbl_Group as tbl 
                                                inner join Lang_Group as lang 
                                                on tbl.GroupID=lang.GroupID";

        public static readonly string GET_LIST_COMPANYID = @"
                                                SELECT tbl.GroupID, tbl.CompanyID, c.CompanyName, lang.GroupName, lang.GroupDescription,              tbl.CreationDate, lang.LangID  
                                                FROM Tbl_Group AS tbl 
                                                INNER JOIN Lang_Group AS lang ON tbl.GroupID = lang.GroupID
                                                INNER JOIN Tbl_Company AS c ON tbl.CompanyID = c.CompanyID
                                                WHERE tbl.CompanyID = @CompanyID;";

        public static readonly string REMOVE = @"DELETE FROM dbo.Lang_Group 
                                                WHERE GroupID = @id
                                                
                                                DELETE FROM dbo.Tbl_Group 
                                                WHERE GroupID = @id";

        public static readonly string UPDATE = @"
                update Tbl_Group set CreationDate=@CreationDate, CompanyID=@CompanyID
                OUTPUT INSERTED.GroupID
                where GroupID=@GroupID";

        public static readonly string LANG_UPDATE = @"
                update Lang_Group  set GroupName=@GroupName, GroupDescription=@GroupDescription
                where GroupID=@GroupID and LangID=@LangID ";

        //gelen userıd ye göre kullanıcının girmiş olduğu gruplar sorgusu
        public static readonly string USERGROUP_BYUSERID = @"SELECT g.GroupID,G.CreationDate,G.CompanyID ,                                                                  lg.GroupDescription,lg.GroupLangID,lg.GroupName,lg.LangID,
                                                c.CompanyName,lang.LangName,ug.UserGroupID
                                                            FROM Lkp_UserGroup ug
                                                            INNER JOIN Tbl_Group g ON ug.GroupID = g.GroupID
                                                            INNER JOIN Lang_Group lg ON g.GroupID = lg.GroupID
                                                            INNER JOIN Tbl_Company c ON g.CompanyID = c.CompanyID
                                                            INNER JOIN Tbl_Language lang ON lg.LangID = lang.LangID
                                                            WHERE ug.UserID = @UserID;";

        //gelen groupID ye göre grupta bulunan kullanıcıları getiren sorgu
        public static readonly string UserGroup_BYGroupID = @"SELECT u.*, c.CompanyName, lul.LevelName,ug.UserGroupID
                                                            FROM Lkp_UserGroup ug
                                                            INNER JOIN Tbl_Users u ON ug.UserID = u.UserID
                                                            INNER JOIN Tbl_Company c ON u.CompanyID = c.CompanyID
                                                            INNER JOIN Lang_UserLevel lul ON u.LevelID = lul.LevelID
                                                            WHERE ug.GroupID=@GroupID;";

        //userıd ve role ıd ye göre kişinin girmiş olduğu grupta istenilen rol varmı yokmu sorgusu;
        public static readonly string USERGROUP_ROLE_CHECK = @"SELECT g.*
                                                            FROM Tbl_Users u
                                                            INNER JOIN Lkp_UserGroup ug ON u.UserID = ug.UserID
                                                            INNER JOIN Tbl_Group g ON ug.GroupID = g.GroupID
                                                            INNER JOIN Lkp_GroupRole gr ON g.GroupID = gr.GroupID
                                                            WHERE u.UserID =@UserID AND gr.RoleID =@RoleID;";

        //GroupRole Query
        public static readonly string GroupRoleADD = @"
                                        INSERT INTO dbo.Lkp_GroupRole 
                                        VALUES (@GroupID, @RoleID)";

        public static readonly string GroupRoleRemove = @"
                                        DELETE From dbo.Lkp_GroupRole 
                                        Where GroupID=@GroupID and RoleID=@RoleID";

        //UserGroup Query

        public static readonly string UserGroupADD = @"
                                        INSERT INTO dbo.Lkp_UserGroup 
                                        VALUES (@UserID, @GroupID)";

        public static readonly string UserGroupRemove = @"
                                        Delete From dbo.Lkp_UserGroup 
                                        where  UserGroupID=@UserGroupID";
    }
}
