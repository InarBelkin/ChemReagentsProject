using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Models
{
    public class SolutionM : IModel<Solution>
    {
        private int? recipeId;
        public int? RecipeId { get => recipeId; set { recipeId = value; if (recipeId != null) RecipeName = ""; OnPropertyChanged(); } }
        private string recipeName;
        public string RecipeName { get => recipeName; set { recipeName = value; OnPropertyChanged(); } }

        private int? concentrationId;
        public int? ConcentrationId { get => concentrationId; set { concentrationId = value; if (concentrationId != null) ConcentrName = ""; OnPropertyChanged(); } }
        private string concentrName;
        public string ConcentrName { get => concentrName; set { concentrName = value; OnPropertyChanged(); } }
        private string gost;
        public string GOST { get => gost; set { gost = value; OnPropertyChanged(); } }
        private decimal coefCorrect;
        public decimal CoefCorrect { get => coefCorrect; set { coefCorrect = value; OnPropertyChanged(); } }

        private DateTime dateBegin;
        public DateTime Date_Begin { get => dateBegin; set { dateBegin = value; OnPropertyChanged(); } }
        private DateTime dateEnd;
        public DateTime Date_End { get => dateEnd; set { dateEnd = value; OnPropertyChanged(); } }

        private decimal count;
        public decimal Count { get => count; set { count = value; OnPropertyChanged(); } }

        public SolutionM() { }
        public SolutionM(Solution s) { setfromDal(s); }

        internal override void setfromDal(Solution item)
        {
            Id = item.Id;
            RecipeId = item.RecipeId;
            ConcentrationId = item.ConcentrationId;
            RecipeName = item.RecipeName;
            ConcentrName = item.ConcentrName;
            GOST = item.GOST;
            CoefCorrect = item.CoefCorrect;
            Date_Begin = item.Date_Begin;
            Date_End = item.Date_End;
            Count = item.Count;
        }

        internal override void updDal(Solution item)
        {
            item.Id = Id;
            item.ConcentrationId = ConcentrationId;
            item.RecipeId = RecipeId;
            item.RecipeName = RecipeName;
            item.ConcentrName = ConcentrName;
            item.GOST = GOST;
            item.CoefCorrect = CoefCorrect;
            item.Date_Begin = Date_Begin;
            item.Date_End = Date_End;
            item.Count = Count;
        }
    }
}
