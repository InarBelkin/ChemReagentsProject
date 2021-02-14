using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Tables;

namespace BLL.Models
{
    public class ConcentrationM : IModel<Concentration>
    {
        private int solutionRecipeId;
        public int SolutionRecipeId { get => solutionRecipeId; set { solutionRecipeId = value; OnPropertyChanged(); } }
        private string name;
        public string Name { get=>name; set { name = value;OnPropertyChanged(); } }
        private decimal count;
        public decimal Count { get => count; set { count = value;OnPropertyChanged(); } }
        public ConcentrationM() { Count = 1000; }
        public ConcentrationM(Concentration c) { setfromDal(c); }
        


        internal override void setfromDal(Concentration item)
        {
            Id = item.Id;
            Name = item.Name;
            Count = item.Count;
            SolutionRecipeId = item.SolutionRecipeId;
        }

        internal override void updDal(Concentration item)
        {
            item.Id = Id;
            item.Name = Name;
            item.Count = Count;
            item.SolutionRecipeId = SolutionRecipeId;
        }
    }
}
