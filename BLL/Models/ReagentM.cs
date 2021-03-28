using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ReagentM : IModel<Reagent>
    {
        private int number;
        public int Number { get => number; set { number = value; OnPropertyChanged(); } }

        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string formula;
        public string Formula { get=>formula; set { formula = value;OnPropertyChanged(); } }


        private string synonimus;
        public string Synonyms { get=>synonimus; set { synonimus = value;OnPropertyChanged(); } }

        private string gost;
        public string GOST { get=>gost; set { gost = value;OnPropertyChanged(); } }

        private string location;
        public string Location { get=>location; set { location = value; OnPropertyChanged(); } }

        private bool isWater;
        public bool IsWater { get => isWater; set { isWater = value; if (!isWater) IsAlwaysWater = false; OnPropertyChanged(); } }

        private bool isAlwaysWater;
        public bool IsAlwaysWater { get => isAlwaysWater; set { isAlwaysWater = IsWater && value; OnPropertyChanged(); } }

        private bool isAccounted;
        public bool IsAccounted { get => isAccounted; set { isAccounted = value; OnPropertyChanged(); } }

        public ReagentM() { }
        public ReagentM(Reagent r)
        {
            setfromDal(r);
        }
        internal override void setfromDal(Reagent r)
        {
            Id = r.Id;
            Number = r.Number;
            Name = r.Name;
            Formula = r.Formula;
            Synonyms = r.Synonyms;
            GOST = r.GOST;
            Location = r.Location;
            IsWater = r.isWater;
            IsAlwaysWater = r.isAlwaysWater;
            IsAccounted = r.IsAccounted;
        }

        internal override void updDal(Reagent item)
        {
            item.Id = Id;
            item.Number = Number;
            item.Name = Name;
            item.Formula = Formula;
            item.Synonyms = Synonyms;
            item.GOST = GOST;
            item.Location = Location;
            item.isWater = isWater;
            item.isAlwaysWater = IsAlwaysWater;
            item.IsAccounted = IsAccounted;
        }




    }
}
