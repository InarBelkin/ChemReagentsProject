using BLL.Interfaces;
using ChemReagentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.PageReziepe
{
    class ReziepeVM : IRecognizable, INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        public ReziepeVM(IDbCrud cr, IReportServ report)
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
    }
}
