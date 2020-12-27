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
        public DateTime DateUse { get; set; }
        public string FromWhere { get; set; }
        public bool IsConsump { get; set; }
        public float Count { get; set; }

        public SupplyStringM() { }
        public SupplyStringM(Solution_line sl)
        {
            Id = sl.Id;
            SupplyId = (int) sl.SupplyId;
            if(sl.Solution.ConcentrationId==null)
            {
                NameTarget = sl.Solution.RecipeName + " " + sl.Solution.ConcentrName;
            }
            else
            {
                NameTarget =
                sl.Solution.Concentration.Solution_Recipe.Name + " " +
                sl.Solution.Concentration.Name;
            }
            DateUse = sl.Solution.Date_Begin;
            Count = sl.Count;
            FromWhere = "Из раствора";
            IsConsump = false;

        }
        public SupplyStringM (Supply_consumption sc)
        {
            Id = sc.Id;
            SupplyId = (int)sc.SupplyId;
            NameTarget = sc.Name;
            DateUse = sc.DateBegin;
            Count = sc.Count;

            FromWhere = "Просто берём";
            IsConsump = true;
        }
    }
}
