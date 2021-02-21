using BLL.Models;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.FIlters
{
    class SupplyComparer : IComparer<SupplyM>
    {
        public int Compare(SupplyM x, SupplyM y)
        {
            if (x.DateExpiration > y.DateExpiration) return 1;
            else if (x.DateExpiration < y.DateExpiration) return -1;
            else return 0;
        }
    }

    class SupplyDALComparer : IComparer<Supply>
    {
        public int Compare(Supply x, Supply y)
        {
            if (x.Date_Expiration > y.Date_Expiration) return 1;
            else if (x.Date_Expiration < y.Date_Expiration) return -1;
            else return 0;
        }
    }

    class SolutComparer : IComparer<SolutionM>
    {
        public int Compare(SolutionM x, SolutionM y)
        {
            if (x.Date_Begin > y.Date_Begin) return 1;
            else if (x.Date_Begin < y.Date_Begin) return -1;
            else return 0;
        }
    }
}
