using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Tables;
namespace BLL.Models
{
    public class SupplyM
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int SupplierId { get; set; }
        public DateTime Date_Begin { get; set; }
        public DateTime Date_End { get; set; }
        public float Count { get; set; }

        public SupplyM() { }
        public SupplyM(Supply s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            SupplierId = s.SupplierId;
            Date_Begin = s.Date_Begin;
            Date_End = s.Date_End;
            Count = s.count;
        }
    }
}
