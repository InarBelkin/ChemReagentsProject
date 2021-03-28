using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RecipeM : IModel<Solution_recipe>
    {
        private string name;
        public string Name { get => name; set { name = value;OnPropertyChanged(); } }
        private string gost;
        public string GOST { get => gost; set { gost = value;OnPropertyChanged(); } }
        public RecipeM() { }
        public RecipeM(Solution_recipe r) { setfromDal(r); }

        internal override void setfromDal(Solution_recipe item)
        {
            Id = item.Id;
            Name = item.Name;
            GOST = item.GOST;
        }

        internal override void updDal(Solution_recipe item)
        {
            item.Id = Id;
            item.Name = Name;
            item.GOST = GOST;
        }
    }
}
