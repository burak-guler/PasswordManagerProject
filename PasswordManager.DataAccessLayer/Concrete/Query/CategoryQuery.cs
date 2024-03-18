using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public static class CategoryQuery
    {
        public static readonly string ADD = @"
                                                INSERT INTO dbo.Tbl_Category (CompanyID, CreationDate, IsActive) 
                                                VALUES (@CompanyID,@CreationDate,@IsActive);

                                                INSERT INTO dbo.Lang_Category (CategoryID, LangID, CategoryName)
                                                VALUES (@@identity, @LangID, @CategoryName);";

        public static readonly string GET = @"
                                                select tbl.CategoryID,lang.CategoryName,lang.LangID, tbl.CompanyID,                         tbl.CreationDate,tbl.IsActive 
                                                from Tbl_Category as tbl 
                                                inner join Lang_Category as lang 
                                                on tbl.CategoryID=lang.CategoryID 
                                                where tbl.CategoryID=@id and tbl.IsActive=1";

        public static readonly string GET_LIST = @"
                                                select tbl.CategoryID,lang.CategoryName,lang.LangID,  tbl.CompanyID,                        tbl.CreationDate,tbl.IsActive from Tbl_Category as tbl 
                                                inner join Lang_Category as lang 
                                                on tbl.CategoryID=lang.CategoryID 
                                                where tbl.IsActive=1";

        public static readonly string GET_LIST_COMPANYID = @"
                                                select tbl.CategoryID,lang.CategoryName, tbl.CompanyID,                                    tbl.CreationDate,tbl.IsActive,lang.LangID 
                                                from Tbl_Category as tbl 
                                                inner join Lang_Category as lang 
                                                on tbl.CategoryID=lang.CategoryID 
                                                where tbl.IsActive=1 and tbl.CompanyID=@companyId";

        public static readonly string REMOVE = "update Tbl_Category set IsActive=0 where CategoryID=@id";

        public static readonly string UPDATE = @"
                                                update Tbl_Category set CompanyID=@CompanyID , CreationDate=@CreationDate
                                                where CategoryID=@CategoryID 

                                                update Lang_Category set CategoryName=@CategoryName
                                                where CategoryID=@CategoryID and LangID=@LangID";
        }
}
