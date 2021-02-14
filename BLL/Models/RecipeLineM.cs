using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Tables;

namespace BLL.Models
{
    public class RecipeLineM : IModel<Solution_recipe_line>
    {
        private int reagentId;
        public int ReagentId { get => reagentId; set { reagentId = value; OnPropertyChanged(); } }
        private int concentracionId;
        public int ConcentracionId { get => concentracionId; set { concentracionId = value; OnPropertyChanged(); } }
        private decimal count;
        public decimal Count { get => count; set { count = value; OnPropertyChanged(); } }
        private string units;
        public string Units { get => units; set { units = value; OnPropertyChanged(); } }
        public RecipeLineM() { }
        public RecipeLineM(Solution_recipe_line s) { setfromDal(s); }

        public void SetReagent(ReagentM reag)
        {
            ReagentId = reag.Id;
            if(reag.IsWater) Units = "мл.";
            else Units = "гр.";
        }

        internal override void setfromDal(Solution_recipe_line item)
        {
            Id = item.Id;
            ReagentId = item.ReagentId;
            ConcentracionId = item.ConcentracionId;
            Count = item.Count;
            if (item.Reagent.isWater) Units = "мл.";
            else Units = "гр.";
        }

        internal override void updDal(Solution_recipe_line item)
        {
            item.Id = Id;
            item.ReagentId = ReagentId;
            item.ConcentracionId = ConcentracionId;
            item.Count = Count;
        }
    }
}
