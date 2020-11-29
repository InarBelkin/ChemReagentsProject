using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.ViewModel
{
    class ReagentsVM: INotifyPropertyChanged
    {
        IDbCrud dbOp;

        public ReagentsVM(IDbCrud cr)
        {
            dbOp = cr;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<ReagentM> reagentlist;
        public List<ReagentM> ReagentList
        {
            get
            {
                return dbOp.Reagents.GetList();
            }
        }

        public List<SupplyM> SuppliesList
        {
            get
            {
                return dbOp.Supplies.GetList();
            }
        }
    }
}
