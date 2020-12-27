using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Additional
{
    class SupplDateComparer : IComparer<SupplyM>
    {
        public int Compare(SupplyM x, SupplyM y)
        {
            if (x.Date_End < y.Date_End) return -1;
            else if (x.Date_End > y.Date_End) return 1;
            return 0;
        }
    }
}
