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

        public Visibility EditModeVis { get => Settings.DevMode ? Visibility.Visible : Visibility.Hidden; }
        private ObservableCollection<ReagentM> reagentlist;
        public ObservableCollection<ReagentM> ReagentList
        {
            get
            {
                reagentlist = dbOp.Reagents.GetList(new ReagentFilter() { IsAccounting = IsAccounting });
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
                OnPropertyChanged("SuppliesList");

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
                    else
                    {
                        newreag.PropertyChanged += Reag_PropertyChanged;
                        if (!newreag.IsAccounted)
                        {
                            var ex2 = dbOp.Supplies.Create(new SupplyM() { ReagentId = newreag.Id, DateStartUse = new DateTime(2000, 1, 1), DateUnWrite = new DateTime(2100, 1, 1),
                                DateProduction = new DateTime(2000, 1, 1), DateExpiration = new DateTime(2100, 1, 1) }) ;
                            if (ex2 != null) new ErrorWin(ex2).ShowDialog();
                        }
                    }
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

        public ObservableCollection<SupplyM> SuppliesList
        {
            get
            {
                SelectSuppl = null;
                if (SelectReag != null && SelectReag.Id != 0)
                {
                    return dbOp.Supplies.GetList(new SupplyFilter() { ReagentId = selectReag.Id });
                }
                else return new ObservableCollection<SupplyM>();
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
                                OnPropertyChanged("SuppliesList");
                            }
                            break;
                        case "Add":
                            if (SelectReag != null)
                            {
                                SupplyM s = new SupplyM();
                                s.ReagentId = SelectReag.Id;
                                if (new WinSupply(dbOp, rep, s).ShowDialog() == true)
                                {
                                    OnPropertyChanged("SuppliesList");
                                }
                            }
                            break;
                        case "Delete":
                            if (SelectReag != null && SelectSuppl != null)
                            {
                                if (new MyDialogWin("Вообще-то удалять поставки нельзя, но если очень хочется то можно. Удалить?", true).ShowDialog() == true)
                                {
                                    dbOp.Supplies.Delete(SelectSuppl.Id);
                                }
                                OnPropertyChanged("SuppliesList");
                            }
                            break;
                        case "UnWrite":
                            if (SelectSuppl != null && SelectSuppl.Active == false && new MyDialogWin("Точно убрать из списанных?", true).ShowDialog() == true)
                            {
                                SelectSuppl.Active = true;
                                dbOp.Supplies.Update(SelectSuppl);
                                OnPropertyChanged("SuppliesList");
                            }
                            break;
                        case "Write":
                            if (SelectSuppl != null && SelectSuppl.Active && new MyDialogWin("Точно списать? Обычно это делается в отчёте", true).ShowDialog() == true)
                            {
                                SelectSuppl.Active = false;
                                dbOp.Supplies.Update(SelectSuppl);
                                OnPropertyChanged("SuppliesList");
                            }
                            break;
                    }
                }));
            }

        }

    }
}
