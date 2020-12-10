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

namespace ChemReagentsProject.ViewModel
{
    class ReagentsVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;

        public ReagentsVM(IDbCrud cr, IReportServ report)
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
                reagentlist.CollectionChanged += Reagents_CollectionChanged;
                foreach (ReagentM r in reagentlist)
                {
                    r.PropertyChanged += Reag_PropCh;
                }

                return reagentlist;
            }
        }

        private void Reag_PropCh(object sender, PropertyChangedEventArgs e)
        {
            ReagentM r = sender as ReagentM;
            dbOp.Reagents.Update(r);
        }

        private void Reagents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ReagentM newreag = e.NewItems[0] as ReagentM;
                    if (newreag.Name == null) newreag.Name = "";
                    if (newreag.Units == null) newreag.Units = "";
                    dbOp.Reagents.Create(newreag);
                    OnPropertyChanged("ReagentList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (ReagentM oldreag in e.OldItems)
                    {
                        dbOp.Reagents.Delete(oldreag.Id);
                    }
                    OnPropertyChanged("ReagentList");
                    break;
            }
        }



        public object SelectReag        //выделили строку
        {
            get
            {
                return new object();
            }
            set
            {
                if (value is ReagentM r)
                {
                    if (r.Id != 0) //костыль
                    {
                        SuppliesList = rep.SupplyByReag(r.Id);
                    }

                }
            }
        }

        private ObservableCollection<SupplyM> suppliesList;
        public ObservableCollection<SupplyM> SuppliesList
        {
            get
            {
                return suppliesList;
            }
            set
            {
                suppliesList = value;
                OnPropertyChanged("SuppliesList");
            }

        }
    }
}
