using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class LogQuery
    {
        public static readonly string ADD = @"
                                        INSERT INTO dbo.Tbl_Log 
                                        VALUES (@CompanyID, @LogDate,@LogDetail)";

        public static readonly string GET = @"SELECT * FROM dbo.Tbl_Log WHERE LogID=@id";

        public static readonly string GET_LIST = @"SELECT * FROM dbo.Tbl_Log";

        public static readonly string GET_LIST_COMPANYID = @"SELECT * FROM dbo.Tbl_Log where CompanyID=@companyId";

        public static readonly string REMOVE = @"DELETE FROM dbo.Tbl_Log WHERE LogID = @id";

        public static readonly string UPDATE = @"
                                    UPDATE dbo.Tbl_Log 
                                    SET CompanyID = @CompanyID,LogDate = @LogDate,LogDetail = @LogDetail
                                    WHERE LogID =@LogID";

    }
}
