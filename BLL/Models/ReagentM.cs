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
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private int number;
        public int Number
        {
            get => number;
            set
            {
                number = value; OnPropertyChanged();
            }
        }

        private bool isWater;
        public bool IsWater
        {
            get => isWater;
            set
            {
                isWater = value;
                if (!isWater) IsAlwaysWater = false;
                OnPropertyChanged();
            }
        }

        private bool isAlwaysWater;
        public bool IsAlwaysWater
        {
            get => isAlwaysWater; set
            {
                if (IsWater && value) isAlwaysWater = true;
                else isAlwaysWater = false;
                OnPropertyChanged();
            }
        }

        private bool isAccounted;
        public bool IsAccounted { get => isAccounted; set { isAccounted = value;OnPropertyChanged(); } }

        public ReagentM() { }
        public ReagentM(Reagent r)
        {
            setfromDal(r);
        }
        internal override void setfromDal(Reagent r)
        {
            Id = r.Id;
            Name = r.Name;
            Number = r.Number;
            IsWater = r.isWater;
            IsAlwaysWater = r.isAlwaysWater;
            IsAccounted = r.IsAccounted;
        }

        internal override void updDal(Reagent item)
        {
            item.Id = Id;
            item.Name = Name;
            item.Number = Number;
            item.isWater = isWater;
            item.isAlwaysWater = IsAlwaysWater;
            item.IsAccounted = IsAccounted;
        }




    }
}
