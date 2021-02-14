using BLL.Interfaces;
using BLL.Models;
using BLL.Services.FIlters;
using ChemReagents.Additional;
using ChemReagents.AdditionalWins.SettingsWin;
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
using System.Windows;

namespace ChemReagents.Pages.ReagentsPage
{
    class ReagentVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        IPageReag Page;

        public ReagentVM(IDBCrud cr, IReportServ report, IPageReag page)
        {
            dbOp = cr;
            rep = report;
            EditMode = false;
            isAccounting = true;
            Page = page;
            Page.setDevMode(Settings.DevMode);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool editMode;
        public bool EditMode { get => editMode; set { editMode = value; OnPropertyChanged("IsRead"); } }
        public bool IsRead { get => !editMode; }

        private bool isAccounting;
        public bool IsAccounting { get => isAccounting; set { isAccounting = value; OnPropertyChanged("ReagentList"); } }

        private ObservableCollection<ReagentM> reagentlist;
        public ObservableCollection<ReagentM> ReagentList
        {
            get
            {
                reagentlist = dbOp.Reagents.GetList(new ReagentFilter() { IsAccounting = IsAccounting }) ;
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
            if (r.Validate())
            {
                var ex = dbOp.Reagents.Update(r);
                if (ex != null) new ErrorWin(ex).ShowDialog();
            }
            else
            {
                new MyDialogWin("Какие-то данные неверны, они не сохранены", false).ShowDialog();
                OnPropertyChanged("ReagentList");
            }


        }

        private void Reagentlist_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ReagentM newreag = e.NewItems[0] as ReagentM;
                    newreag.IsAccounted = IsAccounting;
                    var ex = dbOp.Reagents.Create(newreag);
                    if (ex != null)
                    {
                        new ErrorWin(ex).ShowDialog();
                    }
                    else newreag.PropertyChanged += Reag_PropertyChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ReagentM delreag = e.OldItems[0] as ReagentM;
                    if (dbOp.Supplies.GetList(new SupplyFilter() { ReagentId = delreag.Id }).Count > 0)
                    {
                        if (new MyDialogWin($"У этого реактива ({delreag.Name}) есть поставки. \n" +
                            $"Вы точно хотите удалить его вместе со всеми этими поставками, рецептами и растворами, изготовленными из этих поставок?", true).ShowDialog() == true)
                        {
                            dbOp.Reagents.Delete(delreag.Id);
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
                            if (SelectSuppl != null && new WinSupply(dbOp, rep, SelectSuppl).ShowDialog() == true)
                            {
                                SuppliesList = dbOp.Supplies.GetList(new SupplyFilter() { ReagentId = selectReag.Id });
                            }
                            break;
                        case "Add":
                            if (SelectReag != null )
                            {
                                SupplyM s = new SupplyM();
                                s.ReagentId = SelectReag.Id;
                                if (new WinSupply(dbOp, rep, s).ShowDialog() == true)
                                {
                                    SuppliesList = dbOp.Supplies.GetList(new SupplyFilter() { ReagentId = selectReag.Id });
                                }
                            }
                            break;
                    }
                }));
            }

        }

    }
}
