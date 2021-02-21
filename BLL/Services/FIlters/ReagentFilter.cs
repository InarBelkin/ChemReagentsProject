using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.FIlters
{
    public class ReagentFilter
    {
        public bool IsAccounting;
    }

    public class SupplyFilter
    {
        public int ReagentId = 0;
        public DateTime DateFrom = DateTime.Today.AddYears(-5);
    }

    public class SupplyforSolutFilter
    {
        public int ReagentId = 0;
        public DateTime DateNow = DateTime.Today ;
    }
    public class ConcentrationFilter
    {
        public int RecipeId;
    }
    public class RecipeLineFilter
    {
        public int ConcentrationId;
    }

    public class SolutionFilter
    {
        public DateTime DateStart, DateEnd;
     }
    public class SolutionLineFilter
    {
        public int SolutionId;
    }
    public class SupplyStringsFilter
    {
        public int SupplyId;
        public DateTime DateTo = new DateTime(2100, 1, 1);
    }
}
