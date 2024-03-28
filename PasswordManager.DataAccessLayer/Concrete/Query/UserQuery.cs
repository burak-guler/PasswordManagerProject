namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class UserQuery
    {
        public static readonly string ADD = @"
                                        INSERT INTO dbo.Tbl_Users 
                                        VALUES (@UserName, @Password,@CompanyID,@LevelID,@CreationDate,@IsActive)";

        public static readonly string GET = @"SELECT u.UserID,u.UserName,u.Password, u.CompanyID,u.LevelID,
                                                            u.CreationDate,u.IsActive,lang.LevelName,
                                                            c.CompanyName  FROM dbo.Tbl_Users as u
                                                            inner join Tbl_Company as c on c.CompanyID=u.CompanyID
                                                            inner join Lang_UserLevel as lang on lang.LevelID=u.LevelID
                                                            WHERE UserID=@id";

        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_Users where IsActive=1";

        public static readonly string GET_LIST_COMPANYID = @"SELECT u.UserID,u.UserName,u.Password, u.CompanyID,u.LevelID,
                                                            u.CreationDate,u.IsActive,lang.LevelName,
                                                            c.CompanyName  FROM dbo.Tbl_Users as u
                                                            inner join Tbl_Company as c on c.CompanyID=u.CompanyID
                                                            inner join Lang_UserLevel as lang on lang.LevelID=u.LevelID
                                                            where u.IsActive=1 and u.CompanyID=@CompanyID;";


        public static readonly string REMOVE = @"update dbo.Tbl_Users set IsActive=0 WHERE UserID = @id";        

        public static readonly string UPDATE = @"
                                    UPDATE dbo.Tbl_Users 
                                    SET UserName = @UserName,Password = @Password,CompanyID = @CompanyID,LevelID = @LevelID, CreationDate = @CreationDate WHERE UserID =@UserID";

        public static readonly string LOGIN = @"SELECT u.UserID,u.UserName,u.Password, u.CompanyID,u.LevelID,
                                                            u.CreationDate,u.IsActive,lang.LevelName,
                                                            c.CompanyName  FROM dbo.Tbl_Users as u
                                                            inner join Tbl_Company as c on c.CompanyID=u.CompanyID
                                                            inner join Lang_UserLevel as lang on lang.LevelID=u.LevelID
                                                            where UserName = @UserName and Password = @Password";

        public static readonly string USER_NAME_CHECK = @"select * from Tbl_Users where UserName = @UserName";


        public static readonly string ROLE_CHECK = @"SELECT u.*
                                                    FROM Tbl_Users u
                                                    INNER JOIN Lkp_UserRole ur ON u.UserID = ur.UserID
                                                    WHERE ur.RoleID =@RoleID AND u.UserID =@UserID;";

        //UserRole Queery
        public static readonly string UserRoleADD = @"
                                        INSERT INTO dbo.Lkp_UserRole 
                                        VALUES (@UserID, @RoleID)";

        public static readonly string UserRoleRemove = @"
                                        Delete from dbo.Lkp_UserRole 
                                        where  UserRoleID=@UserRoleID";

        public static readonly string UserRole_GetLıst_UserID = @"
                                        Select * from  dbo.Lkp_UserRole where UserID=@UserID
";


    }
}
