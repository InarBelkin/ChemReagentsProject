using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Pages;
using ChemReagentsProject.Pages.PageReagents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

            var ex = dbOp.Reagents.Update(r);
            if (ex != null) MessageBox.Show(ex.Message);
        }

        private void Reagents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ReagentM newreag = e.NewItems[0] as ReagentM;
                    if (newreag.Name == null) newreag.Name = "";
                    if (newreag.Units == null) newreag.Units = "";
                    var ex = dbOp.Reagents.Create(newreag);
                    if (ex != null) MessageBox.Show(ex.Message);
                    OnPropertyChanged("ReagentList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (ReagentM oldreag in e.OldItems)
                    {
                        ObservableCollection<SupplyM> s = rep.SupplyByReag(oldreag.Id);
                        if (rep.SupplyByReag(oldreag.Id) != null && rep.SupplyByReag(oldreag.Id).Count != 0)
                        {
                            Pages.WinQuestion.QuestWin win = new Pages.WinQuestion.QuestWin("Вы хотите удалить реагент, у которого есть поставки, вы уверены?");
                            if (win.ShowDialog() == true)
                            {
                                dbOp.Reagents.Delete(oldreag.Id);
                            }
                            else
                            {
                                OnPropertyChanged("ReagentList");
                            }
                        }
                        else
                        {
                            dbOp.Reagents.Delete(oldreag.Id);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;

            }
        }

        private ReagentM selectreag;
        public object SelectReag        //выделили строку
        {
            get
            {
                ReagentM reagent = dbOp.Reagents.GetItem(2);
                return reagent;
            }
            set
            {
                if (value is ReagentM r)
                {
                    if (r.Id != 0) //костыль
                    {
                        selectreag = r;
                        SuppliesList = rep.SupplyByReag(r.Id);
                    }
                    //else
                    //{
                    //    MessageBox.Show("Ну что-то с ид");
                    //}

                }
            }
        }

        private ObservableCollection<SupplyM> suppliesList;
        public ObservableCollection<SupplyM> SuppliesList
        {
            get
            {
                if (selectreag != null)
                {
                    var a = rep.SupplyByReag(selectreag.Id);
                    return a;
                }
                // return suppliesList;
                return null;
            }
            set
            {
                //suppliesList = value;
                OnPropertyChanged("SuppliesList");
            }

        }


        private SupplyM selectSuppl;
        public object SelectSuppl
        {
            set
            {
                selectSuppl = value as SupplyM;

            }
        }

        private RelayCommand addSuppl;
        private WinEditSupplies winSuppl;
        public RelayCommand AddSuppl      //
        {
            get
            {
                return addSuppl ?? (addSuppl = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Edit":
                            //rep.SupplyByReag(100);
                            if (selectSuppl != null)
                            {
                                winSuppl = new WinEditSupplies(dbOp, rep, selectSuppl);
                            }
                            else
                            {
                                MessageBox.Show("Сначала выберите поставку для редактирования");
                                return;
                            }
                            break;
                        case "Add":
                            if (selectreag != null)
                            {
                                winSuppl = new WinEditSupplies(dbOp, rep, new SupplyM() { ReagentId = selectreag.Id });
                            }
                            else
                            {
                                winSuppl = new WinEditSupplies(dbOp, rep, new SupplyM());
                            }
                            break;
                        //case "generate":
                        //    var a = DateTime.MinValue;
                        //    var ex = dbOp.Supplies.Create(new SupplyM() { Count = 10, ReagentId = 100, Date_Begin = new DateTime(1700, 12, 12), Date_End = new DateTime(1980, 12, 12) });

                            //var ex2 = dbOp.Supplies.Create(new SupplyM() { Count = 10, Date_Begin = DateTime.Now, Date_End = DateTime.Now, ReagentId = 20, State = SupplStates.Active, SupplierId = 1 });
                           
                            //    var sup = dbOp.Supplies.GetItem(30);
                            //    sup.Date_Begin = new DateTime(1300, 12, 12);
                            //    var ex = dbOp.Supplies.Update(sup);
                            //break;
                        default:
                            break;
                    }
                    winSuppl.ShowDialog();
                    if (winSuppl.DialogResult == true) //как-то обновить
                    {
                        OnPropertyChanged("SuppliesList");

                    }
                }));
            }
        }
    }
}
