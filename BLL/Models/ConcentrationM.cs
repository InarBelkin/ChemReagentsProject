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
    public class ConcentrationM : INotifyPropertyChanged
    {
        private int id;
        private int solutionRecipeId;
        private string name;

        public int Id { get { return id; } set { id = value; } }

        public int SolutionRecipeId
        {
            get => solutionRecipeId;
            set
            {
                solutionRecipeId = value;
                OnPropertyChanged();
            }
        }

        public string NameOfConc
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public ConcentrationM() { NameOfConc = ""; }
        public ConcentrationM(Concentration c)
        {
            Id = c.Id;
            SolutionRecipeId = c.SolutionRecipeId;
            NameOfConc = c.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Concentration getDal()
        {
            Concentration c = new Concentration();
            c.Id = Id;
            c.SolutionRecipeId = SolutionRecipeId;
            c.Name = NameOfConc;
            return c;
        }

        internal void updDal(Concentration c)
        {
            
            c.Id = Id;

            c.SolutionRecipeId = SolutionRecipeId;
            c.Name = NameOfConc;
        }
    }
}
