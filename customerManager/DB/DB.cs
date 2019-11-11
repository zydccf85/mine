using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dos.ORM.MsAccess;
using Dos.ORM;

namespace CustomerManager.DB
{
    class DB
    {
        public static readonly DbSession Content = new DbSession("AccessConn");
    }
}
