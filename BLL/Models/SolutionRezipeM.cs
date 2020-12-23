using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SolutionRezipeM : INotifyPropertyChanged
    {
        private int id;
        private string name;

        public int Id { get { return id; } set { id = value; } }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public SolutionRezipeM() { Name = ""; }
        public SolutionRezipeM(Solution_recipe s)
        {
            Id = s.Id;
            Name = s.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Solution_recipe getDal()
        {
            Solution_recipe s = new Solution_recipe();
            s.Id = Id;
            s.Name = Name;
            return s;
        }
        internal void updDal(Solution_recipe s)
        {
            s.Id = Id;
            s.Name = Name;
            // return s;
        }
    }

    public class SolutRezLineM : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private int reagentid;
        public int ReagentId
        {
            get => reagentid;
            set { reagentid = value; OnPropertyChanged(); }
        }

        private int concentrationId;
        public int ConcentrationId
        {
            get => concentrationId;
            set { concentrationId = value; OnPropertyChanged(); }
        }
        private float сount;
        public float Count
        {
            get => сount;
            set { сount = value; OnPropertyChanged(); }
        }

        private string units;
        public string Units
        {
            get => units;
            set { units = value;OnPropertyChanged(); }
        }



        public SolutRezLineM() { }
        public SolutRezLineM(Solution_recipe_line s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            ConcentrationId = s.ConcentracionId;
            Count = s.Count;
            if (s.Reagent != null)
            {
                Units = s.Reagent.units;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Solution_recipe_line getDal()
        {
            Solution_recipe_line a = new Solution_recipe_line();
            a.Id = Id;
            a.ReagentId = ReagentId;
            a.ConcentracionId = ConcentrationId;
            a.Count = Count;
            return a;

        }
        internal void updDal(Solution_recipe_line l)
        {
            l.Id = Id;
            l.ReagentId = ReagentId;
            l.ConcentracionId = ConcentrationId;
            l.Count = Count;
        }
    }
}
