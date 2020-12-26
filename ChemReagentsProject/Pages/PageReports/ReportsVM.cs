using BLL.Interfaces;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.PageReports
{
    class ReportsVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        public ReportsVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand generateCommand;
        public RelayCommand GenerateCommand
        {
            get
            {
                return generateCommand ?? (generateCommand = new RelayCommand(obj =>
                {

                   
                }));
            }
        }


    }
}
