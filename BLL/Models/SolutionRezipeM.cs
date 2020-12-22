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
    public class SolutionRezipeM : INotifyPropertyChanged
    {
        private int id;
        private string name;

        public int Id { get { return id; } set { id = value; } }
        public string Name
        {
            get
            {
                return name;
            }
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

    public class SolutRezLineM
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int SolutionRecipeId { get; set; }
        public float Count { get; set; }

        public SolutRezLineM() { }
        public SolutRezLineM(Solution_recipe_line s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            SolutionRecipeId = s.SolutionRecipeId;
            Count = s.Count;
        }
        internal Solution_recipe_line getDal()
        {
            Solution_recipe_line a = new Solution_recipe_line();
            a.Id = Id;
            a.ReagentId = ReagentId;
            a.SolutionRecipeId = SolutionRecipeId;
            a.Count = Count;
            return a;

        }
        internal void updDal(Solution_recipe_line l)
        {
            l.Id = Id;
            l.ReagentId = ReagentId;
            l.SolutionRecipeId = SolutionRecipeId;
            l.Count = Count;
        }
    }
}
