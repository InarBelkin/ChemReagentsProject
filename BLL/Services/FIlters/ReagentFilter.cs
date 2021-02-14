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

    public class ConcentrationFilter
    {
        public int RecipeId;
    }
    public class RecipeLineFilter
    {
        public int ConcentrationId;
    }
}
