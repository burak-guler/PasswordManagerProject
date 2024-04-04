using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Concrete.Query
{
    public class LanguageQuery
    {
        public static readonly string GET_LIST = @"select * from  Tbl_Language";
    }
}
