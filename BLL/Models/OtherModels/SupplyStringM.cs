using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.OtherModels
{
    public class SupplyStringM
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public string NameTarget { get; set; }
        public string Count { get; set; }

        public SupplyStringM() { }
        public SupplyStringM(Solution_line sl)
        {
            Id = sl.Id;
            SupplyId = (int) sl.SupplyId;
           // NameTarget = sl.Solution.

        }
        public SupplyStringM (Supply_consumption sc)
        {

        }
    }
}
