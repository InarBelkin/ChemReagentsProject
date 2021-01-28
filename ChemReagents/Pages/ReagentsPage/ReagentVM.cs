using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.ReagentsPage
{
    class ReagentVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;

        public ReagentVM(IDBCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ReagentM> reagentlist;
        public ObservableCollection<ReagentM> ReagentList
        {
            get
            {
                reagentlist = dbOp.Reagents.GetList();
                reagentlist.CollectionChanged += Reagentlist_CollectionChanged;
                foreach(ReagentM reag in reagentlist)
                {
                    reag.PropertyChanged += Reag_PropertyChanged;
                }
                return reagentlist;
            }
        }

        private void Reagentlist_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ReagentM newreag = e.NewItems[0] as ReagentM;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;
            }
        }

        private void Reag_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ReagentM r = sender as ReagentM;
            var ex = dbOp.Reagents.Update(r);
            if (ex != null) ;
        }
    }
}
