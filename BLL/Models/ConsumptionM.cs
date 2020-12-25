using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ConsumptionM : IModel<Supply_consumption>
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public string Name { get; set; }
        public float Count { get; set; }

        public ConsumptionM() { Name = ""; }
        public ConsumptionM(Supply_consumption sc)
        {
            Id = sc.Id;
            SupplyId = sc.SupplyId;
            Name = sc.Name;
            Count = sc.Count;
        }

        public Supply_consumption getDal()
        {
            Supply_consumption sc = new Supply_consumption();
            sc.Id = Id;
            sc.SupplyId = SupplyId;
            sc.Name = Name;
            sc.Count = Count;
            return sc;
        }

        public void updDal(Supply_consumption sc)
        {
            sc.Id = Id;
            sc.SupplyId = SupplyId;
            sc.Name = Name;
            sc.Count = Count;
        }
    }
}
