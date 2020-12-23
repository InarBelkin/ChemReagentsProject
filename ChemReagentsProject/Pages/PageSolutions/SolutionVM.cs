using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.PageSolutions
{
    class SolutionVM: IRecognizable, INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;

        public SolutionVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid GetGuid()
        {
            throw new NotImplementedException();
        }

        private ObservableCollection<SolutionM> solutionList;
        public ObservableCollection<SolutionM> SolutionList
        {
            get
            {
                solutionList = dbOp.Solutions.GetList();
                return solutionList;
            }
        }
    }
}
