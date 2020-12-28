using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Tables;
namespace BLL.Models
{
    public class ReagentM : INotifyPropertyChanged , IModel<Reagent>
    {
        private int id;
        private string name;
        private string units;
        private string warn;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Units
        {
            get { return units; }
            set
            {
                units = value;
                OnPropertyChanged();
            }
        }

        public string Warn
        {
            get => warn;
            set
            {
                warn = value;
                OnPropertyChanged();
            }
        }

        public ReagentM() { }
        public ReagentM(Reagent r)
        {
            Id = r.Id;
            Name = r.Name;
            Units = r.units;

            Console.WriteLine();

        }

        //~ReagentM()
        //{
        //    Console.Beep();
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Reagent getDal()
        {
            Reagent ret = new Reagent();
            ret.Id = Id;
            ret.Name = name;
            ret.units = units;
            return ret;
        }

        public void updDal(Reagent item)
        {
            item.Id = Id;
            item.Name = name;
            item.units = units;
        }
    }
}
