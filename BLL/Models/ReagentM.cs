using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Tables;
namespace BLL.Models
{
    public class ReagentM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
        public virtual List<SupplyM> Supplies { get; set; }

        public ReagentM() { }
        public ReagentM(Reagent r)
        {
            Id = r.Id;
            Name = r.Name;
            Units = r.units;
            foreach(Supply n in r.Supplies)
            {
                Supplies.Add(new SupplyM(n));
            }
        }
    }
}
