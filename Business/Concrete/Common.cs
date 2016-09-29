using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class Common
    {
        protected Db.dbDataContext db = new Db.dbDataContext(ConnectString());

        private static string ConnectString()
        {
            return "Data Source = 114.70.234.168; Initial Catalog = yokydoky; Persist Security Info = True; User ID = yokydoky; Password = yokydoky";
        }
    }
}
