using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SolutionLineM : IModel<Solution_line>
    {
        private int solutionId;
        public int SolutionId { get => solutionId; set { solutionId = value; OnPropertyChanged(); } }
        private int? supplyId;
        public int? SupplyId { get => supplyId; set { supplyId = value; OnPropertyChanged(); } }
        private string nameOtherComponent;
        public string NameOtherComponent { get => nameOtherComponent; set { nameOtherComponent = value; OnPropertyChanged(); } }
        private decimal count;
        public decimal Count { get => count; set { count = value;OnPropertyChanged(); } }

        public SolutionLineM() { }
        public SolutionLineM(Solution_line s) { setfromDal(s); }
        internal override void setfromDal(Solution_line item)
        {
            Id = item.Id;
            SolutionId = item.SolutionId;
            SupplyId = item.SupplyId;
            NameOtherComponent = item.NameOtherComponent;
            Count = item.Count;
        }

        internal override void updDal(Solution_line item)
        {
            item.Id = Id;
            item.SolutionId = SolutionId;
            item.SupplyId = SupplyId;
            item.NameOtherComponent = NameOtherComponent;
            item.Count = Count;
        }
    }
}
