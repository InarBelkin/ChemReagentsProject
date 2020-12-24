using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Models
{
    public class SolutionM : INotifyPropertyChanged
    {
        private int? soltuionRecipeId;
        private int? concentrationId;
        private DateTime date_Begin;

        public int Id { get; set; }
        public int? SolutionRecipeId
        {
            get => soltuionRecipeId;
            set
            {
                soltuionRecipeId = value;
                OnPropertyChanged();
            }
        }
        public int? ConcentrationId
        {
            get => concentrationId;
            set
            {
                concentrationId = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date_Begin
        {
            get => date_Begin;
            set
            {
                date_Begin = value;
                OnPropertyChanged();
            }
        }



        public int SelectBind
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }

      


        public SolutionM() { }
        public SolutionM(Solution s)
        {
            Id = s.Id;
            ConcentrationId = s.ConcentrationId;
            // SolutionRecipeId = s.SolutionRecipeId;
            Date_Begin = s.Date_Begin;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Solution getDal()
        {
            Solution s = new Solution();
            s.Id = Id;
            s.ConcentrationId = ConcentrationId;
            s.Date_Begin = Date_Begin;
            return s;
        }

        internal void updDal(Solution s)
        {
            s.Id = Id;
            s.ConcentrationId = ConcentrationId;
            s.Date_Begin = Date_Begin;
        }


    }

    public class SolutionLineM
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public int SupplyId { get; set; }
        public float Count { get; set; }

        public SolutionLineM() { }
        public SolutionLineM(Solution_line sl)
        {
            Id = sl.Id;
            SolutionId = sl.SolutionId;
            SupplyId = sl.SupplyId;
            Count = sl.Count;
        }

        internal Solution_line getDal()
        {
            Solution_line l = new Solution_line();
            l.Id = Id;
            l.SolutionId = SolutionId;
            l.SupplyId = l.SupplyId;
            l.Count = Count;
            return l;
        }

        internal void updDal(Solution_line l)
        {
            l.Id = Id;
            l.SolutionId = SolutionId;
            l.SupplyId = l.SupplyId;
            l.Count = Count;
        }
    }
}
