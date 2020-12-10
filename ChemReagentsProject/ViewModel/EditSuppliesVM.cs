using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.ViewModel
{
    class EditSuppliesVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;

        public EditSuppliesVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
