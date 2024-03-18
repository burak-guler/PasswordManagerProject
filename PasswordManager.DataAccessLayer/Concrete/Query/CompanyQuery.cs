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
                                            INSERT INTO dbo.Tbl_Company 
                                            VALUES (@CompanyName,@IsActive)";

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
