using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.SupplyWin
{
    class SuppliesWinVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;

        public SuppliesWinVM(IDBCrud cr, IReportServ report, SupplyM sup)
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
