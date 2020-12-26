using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.OtherModels
{
    public class MonthReportM
    {
        public int SupplyId { get; set; }

        public string ReagentName { get; set; }
        

        public float Count { get; set; }
        public string status { get; set; }

        //public string ForWhat { get; set; }
    }
}
   