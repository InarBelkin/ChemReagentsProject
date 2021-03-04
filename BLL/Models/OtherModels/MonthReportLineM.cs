using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.OtherModels
{
    public class MonthReportLineM
    {
        public int SupplyId { get; set; }
        public string ReagentName { get; set; }
        public decimal CountMonth { get; set; }
        public decimal RemainMonth { get; set; }
        public string Units { get; set; }
        public bool IsWrittenOff { get; set; }
        public bool AcceptWriteOff { get; set; }


    }
}
