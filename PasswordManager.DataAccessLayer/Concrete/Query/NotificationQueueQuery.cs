using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class NotificationQueueQuery
    {
        public static readonly string ADD = @"
                                        INSERT INTO dbo.Tbl_NotificationQueue 
                                        VALUES (@IsSent, @UserID,@CompanyID,@CreationDate,@SentDate,@Title,@Body)";

        public static readonly string GET = @"SELECT * FROM dbo.Tbl_NotificationQueue WHERE NotificationQueueID=@id";

        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_NotificationQueue";

        public static readonly string GET_LIST_COMPANYID = @"SELECT * FROM dbo.Tbl_NotificationQueue 
                                                            where CompanyID=@companyId";

        public static readonly string GET_PENDING = @"SELECT TOP 10 * FROM dbo.NotificationQueue WHERE IsSent = 0;";

        public static readonly string REMOVE = @"DELETE FROM dbo.Tbl_NotificationQueue WHERE NotificationQueueID = @id";

        public static readonly string UPDATE = @"
                                    UPDATE dbo.Tbl_NotificationQueue 
                                    SET IsSent = @IsSent,CompanyID = @CompanyID,CreationDate = @CreationDate,Title = @Title,Body = @Body,SentDate=@SentDate,UserID=@UserID
                                    WHERE NotificationQueueID =@NotificationQueueID";
    }
}
