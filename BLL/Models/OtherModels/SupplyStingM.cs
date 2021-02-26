using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SupplyStingM : IModel<Supply_consumption>
    {
        public int SupplyId { get; set; }
        public string Name { get; set; }
        public DateTime DateBegin { get; set; }
        public decimal Count { get; set; }
        public bool IsConsump { get; set; }
        public string FromWhere { get; set; }

        public SupplyStingM() { }

        public SupplyStingM(Supply_consumption sc)
        {
            setfromDal(sc);
            IsConsump = true;
            FromWhere = "На что-то друое";
        }

        public SupplyStingM(Solution_line sl)
        {
            Id = sl.Id;
            //if(sl.Solution.RecipeId==null)
            //{
            //    Name = sl.Solution.RecipeName;
            //}
            //else
            //{
            //    Name = sl.Solution.Recipe.Name;
            //    if(sl.Solution.ConcentrationId!=null)
            //    {
            //        Name = Name +" "+ sl.Solution.Concentration.Name;
            //    }
            //}
            DateBegin = sl.Solution.Date_Begin;
            Count = sl.Count;
            IsConsump = false;
            FromWhere = "На раствор";
        }

        internal override void setfromDal(Supply_consumption item)
        {
            Id = item.Id;
            SupplyId = item.SupplyId;
            Name = item.Name;
            DateBegin = item.DateBegin;
            Count = item.Count;
        }

        internal override void updDal(Supply_consumption item)
        {
            item.Id = Id;
            item.SupplyId = SupplyId;
            item.Name = Name;
            item.DateBegin = DateBegin;
            item.Count = Count;
        }
    }
}
