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
        private int? concentrationId;
        public int? ConcentrationId { get=>concentrationId; set { concentrationId = value;OnPropertyChanged(); } }
        private string recipeName;
        public string RecipeName { get=>recipeName; set { recipeName = value;OnPropertyChanged(); } }
        private string concentrName;
        public string ConcentrName { get=>concentrName; set { concentrName = value;OnPropertyChanged(); } }
        private DateTime dateBegin;
        public DateTime Date_Begin { get => dateBegin; set { dateBegin = value;OnPropertyChanged(); } }

        internal override void setfromDal(Solution item)
        {
            Id = item.Id;
            ConcentrationId = item.ConcentrationId;
            RecipeName = item.RecipeName;
            ConcentrName = item.ConcentrName;
            Date_Begin = item.Date_Begin;
        }

        internal override void updDal(Solution item)
        {
            item.Id = Id;
            item.ConcentrationId = ConcentrationId;
            item.RecipeName = RecipeName;
            item.ConcentrName = ConcentrName;
            item.Date_Begin = Date_Begin;
        }
    }
}
