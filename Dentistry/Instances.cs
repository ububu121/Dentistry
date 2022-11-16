using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentistry
{
    class Instances
    {
        private static dentistryEntities _Db = null;
        public static dentistryEntities db
        {
            get
            {
                if (_Db == null)
                    _Db = new dentistryEntities();
                return _Db;
            }
        }
    }
}
