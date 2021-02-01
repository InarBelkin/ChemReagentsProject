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

        private bool isWater;
        public bool IsWater
        {
            get => isWater;
            set
            {
                isWater = value;
                OnPropertyChanged();
            }
        }

        private decimal density;
        public decimal Density
        {
            get => density;
            set
            {
                density = value;
                OnPropertyChanged();
            }
        }

        public ReagentM() { }
        //public ReagentM(Reagent r)
        //{
        //    setfromDal(r);
        //}
        internal override void setfromDal(Reagent r)
        {
            Id = r.Id;
            Name = r.Name;
            IsWater = r.isWater;
            Density = r.density;
        }

        //internal override Reagent getDal()
        //{
        //    Reagent r = new Reagent();
        //    updDal(r);
        //    return r;
        //}

        internal override void updDal(Reagent item)
        {
            item.Id = Id;
            item.Name = Name;
            item.isWater = isWater;
            item.density = Density;
        }

        
        

    }
}
