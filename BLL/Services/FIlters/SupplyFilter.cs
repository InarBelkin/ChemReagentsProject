using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.FIlters
{
    public class SupplyFilter
    {
        public int ReagentId = 0;
        public DateTime DateFrom = DateTime.Today.AddYears(-5);
    }
}
