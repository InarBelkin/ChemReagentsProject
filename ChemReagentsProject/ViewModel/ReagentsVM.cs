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

        private ObservableCollection<ReagentM> reagentlist;
        public ObservableCollection<ReagentM> ReagentList
        {
            get
            {
                reagentlist = new ObservableCollection<ReagentM>();
                List<ReagentM> a = dbOp.Reagents.GetList();
                foreach(ReagentM r in a )
                {
                    reagentlist.Add(r);
                }
                reagentlist.CollectionChanged += Reagents_CollectionChanged;
                return reagentlist;/*dbOp.Reagents.GetList();*/
            }
        }

        private void Reagents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        public object SelectReag
        {
            get
            {
                return null;
            }
            set
            {
                if(value is ReagentM r)
                {
                    Console.WriteLine();
                }
            }
        }

        public List<SupplyM> SuppliesList
        {
            get
            {
                return dbOp.Supplies.GetList();
            }
            set
            {
                   
            }

        }
    }
}
