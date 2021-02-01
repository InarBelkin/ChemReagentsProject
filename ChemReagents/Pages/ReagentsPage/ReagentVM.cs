using BLL.Interfaces;
using BLL.Models;
using BLL.Services.FIlters;
using ChemReagents.Additional;
using ChemReagents.Pages.DialogWins;
using ChemReagents.Pages.SupplyWin;
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
                foreach (ReagentM reag in reagentlist)
                {
                    reag.PropertyChanged += Reag_PropertyChanged;
                }
                return reagentlist;
            }
        }
        private ReagentM selectReag;
        public ReagentM SelectReag
        {
            get => selectReag;
            set
            {
                selectReag = value;
                OnPropertyChanged("SelectReag");
                if (value != null && value.Id != 0)
                {
                    SuppliesList = dbOp.Supplies.GetList(new SupplyFilter() { ReagentId = selectReag.Id });
                }

            }
        }

        private void Reag_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ReagentM r = sender as ReagentM;
            var ex = dbOp.Reagents.Update(r);
            if (ex != null) new ErrorWin(ex).ShowDialog();
        }

        private void Reagentlist_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ReagentM newreag = e.NewItems[0] as ReagentM;

                    var ex = dbOp.Reagents.Create(newreag);
                    if (ex != null)
                    {
                        new ErrorWin(ex).ShowDialog();
                        newreag.PropertyChanged += Reag_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ReagentM delreag = e.OldItems[0] as ReagentM;
                    if (dbOp.Supplies.GetList(new SupplyFilter() { ReagentId = delreag.Id }).Count > 0)
                    {
                        if (new MyDialogWin($"У этого реактива ({delreag.Name}) есть поставки. \n" +
                            $"Вы точно хотите удалить его вместе со всеми этими поставками, рецептами и растворами, изготовленными из этих поставок?").ShowDialog() == true)
                        {
                            dbOp.Supplies.Delete(delreag.Id);
                        }
                        else
                        {
                            OnPropertyChanged("ReagentList");
                        }
                    }
                    else
                    {
                        dbOp.Reagents.Delete(delreag.Id);
                    }

                    break;
            }
        }


        private ObservableCollection<SupplyM> suppliesList;
        public ObservableCollection<SupplyM> SuppliesList
        {
            get => suppliesList;
            set
            {
                suppliesList = value; SelectSuppl = null; OnPropertyChanged("SuppliesList");
            }
        }
        private SupplyM selectSuppl;
        public SupplyM SelectSuppl
        {
            get => selectSuppl;
            set { selectSuppl = value; OnPropertyChanged("SelectSuppl"); }
        }

        private RelayCommand addSuppl;
        public RelayCommand AddSuppl
        {
            get
            {
                return addSuppl ?? (addSuppl = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Edit":
                            if(new WinSupply( dbOp, rep, SelectSuppl).ShowDialog()== true)
                            {

                            }

                            break;
                        case "Add":

                            break;
                    }
                }));
            }

        }

    }
}
