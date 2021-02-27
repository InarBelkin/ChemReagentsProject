using BLL.Interfaces;
using BLL.Models.OtherModels;
using ChemReagents.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.ReportPage
{
    class ReportVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        public ReportVM(IDBCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
            SelYear = (uint)DateTime.Today.Year;
            SelectMonth = (byte)DateTime.Today.Month;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private uint selYear;
        public uint SelYear { get => selYear; set => selYear = value; }

        public MonthM[] MonthList => rep.GetMonths();
        private byte selectMonth;
        public byte SelectMonth { get => selectMonth; set { selectMonth = value; OnPropertyChanged("SelectMonth"); } }

        private RelayCommand create;
        public RelayCommand Create
        {
            get => create ?? (create = new RelayCommand(obj =>
            {
                if(rep.GetMonthRep(SelYear, SelectMonth) == null)   //если не нашли рецепт, то можно его создать
                {

                }
            }));
        }
    }
}
