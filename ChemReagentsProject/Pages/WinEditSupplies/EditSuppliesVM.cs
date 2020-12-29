using BLL.Interfaces;
using BLL.Models;
using BLL.Models.OtherModels;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.Pages.WinEditConsumption;
using ChemReagentsProject.Pages.WinQuestion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.ViewModel
{
    class EditSuppliesVM : INotifyPropertyChanged, IRecognizable
    {
        IDbCrud dbOp;
        IReportServ rep;
        SupplyM EditSuppl;
        Guid ThisGuid;
        public EditSuppliesVM(IDbCrud cr, IReportServ report, SupplyM isupl)
        {
            ThisGuid = Guid.NewGuid();

            dbOp = cr;
            rep = report;
            listReag = dbOp.Reagents.GetList();
            listSuppliers = dbOp.Suppliers.GetList();
            if (isupl.Id < 0)
            {
                EditSuppl = new SupplyM()
                {
                    Id = -1,
                    ReagentId = isupl.ReagentId,
                    Date_Begin = DateTime.Now,
                    Date_End = DateTime.Now,
                    Count = 0,
                };
                foreach (ReagentM reag in listReag)
                {
                    if (reag.Id == EditSuppl.ReagentId)
                    {
                        selectreag = reag;
                        break;
                    }
                }
            }
            else
            {
                EditSuppl = dbOp.Supplies.GetItem(isupl.Id);
                foreach (ReagentM reag in listReag)
                {
                    if (reag.Id == EditSuppl.ReagentId)
                    {
                        selectreag = reag;
                        break;
                    }
                }
                foreach (SupplierM sur in listSuppliers)
                {
                    if (sur.Id == EditSuppl.SupplierId)
                    {
                        selectSupplier = sur;
                        break;
                    }
                }


            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid GetGuid()
        {
            return ThisGuid;
        }

        public int SupId
        {
            get
            {
                return EditSuppl.Id;
            }
        }

        private ObservableCollection<ReagentM> listReag;
        public ObservableCollection<ReagentM> ListReag
        {
            get
            {
                return listReag;
            }
        }

        private ReagentM selectreag;
        public ReagentM SelectReag
        {
            get
            {
                return selectreag;
                //return dbOp.Reagents.GetItem(2);
            }
            set
            {
                selectreag = value;
                EditSuppl.ReagentId = value.Id;

            }
        }

        private ObservableCollection<SupplierM> listSuppliers;
        public ObservableCollection<SupplierM> ListSuppliers
        {
            get
            {
                return listSuppliers;
            }
        }

        private SupplierM selectSupplier;
        public SupplierM SelectSupplier
        {
            get
            {
                return selectSupplier;
            }
            set
            {
                selectSupplier = value;
                EditSuppl.SupplierId = value.Id;
            }
        }

        public DateTime SupDTBeg
        {
            get => EditSuppl.Date_Begin;
            set => EditSuppl.Date_Begin = value;
        }

        public DateTime SupDTEnd
        {
            get => EditSuppl.Date_End;
            set => EditSuppl.Date_End = value;
        }

        public float SupAmount
        {
            get => EditSuppl.Count;
            set { EditSuppl.Count = value; OnPropertyChanged("SupplyStringsList"); }
        }

        private RelayCommand comButton;
        public RelayCommand ComButton      //
        {
            get
            {
                return comButton ?? (comButton = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Save":
                            if (SupAmount >= 0 && SupDTBeg < SupDTEnd)
                            {
                                if (EditSuppl.Validate())
                                {
                                    if (EditSuppl.Id == -1)    // если создаётся заново
                                    {
                                        var ex = dbOp.Supplies.Create(EditSuppl);
                                        if (ex != null) MessageBox.Show(ex.Message);
                                    }
                                    else
                                    {
                                        var ex = dbOp.Supplies.Update(EditSuppl);
                                        if (ex != null) MessageBox.Show(ex.Message);
                                    }
                                    WindowService.CloseWindow(ThisGuid, true);
                                }
                                else
                                {
                                    MessageBox.Show("Что-то не довведено");
                                }
                            }
                            else MessageBox.Show("Что-то неправильно введено");
                       



                            break;
                        case "Cancel":
                            WindowService.CloseWindow(ThisGuid, false);
                            break;
                        default:
                            break;
                    }



                }));
            }
        }

        private float remainder;
        public float Remainder { get => remainder; set { remainder = value; OnPropertyChanged("Remainder"); } }

        public List<SupplyStringM> SupplyStringsList
        {
            get
            {
                var a = rep.GetSupplyStrings(EditSuppl.Id);
                Remainder = SupAmount - a.Summ;
                return a.Item1;
            }
        }

        private SupplyStringM selectString;
        public SupplyStringM SelectString { get => selectString; set { selectString = value; OnPropertyChanged("SelectString"); } }

        private RelayCommand consumpButton;
        public RelayCommand ConsumpButton      //
        {
            get
            {
                return consumpButton ?? (consumpButton = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Add":

                            ConsumptionM cons = new ConsumptionM() { SupplyId = EditSuppl.Id };
                            WinEditConsumption win = new WinEditConsumption(dbOp, rep, cons);
                            if (win.ShowDialog() == true)
                            {
                                dbOp.Consumptions.Create(cons);
                                OnPropertyChanged("SupplyStringsList");
                            }
                            break;
                        case "Change":
                            if (SelectString != null)
                            {
                                if (SelectString.IsConsump==true)
                                {
                                    ConsumptionM cons2 = dbOp.Consumptions.GetItem(SelectString.Id);
                                    WinEditConsumption win2 = new WinEditConsumption(dbOp, rep, cons2);
                                    if (win2.ShowDialog() == true)
                                    {
                                        dbOp.Consumptions.Update(cons2);
                                        OnPropertyChanged("SupplyStringsList");
                                    }
                                }
                                else MessageBox.Show("Это строка из раствора");
                            }
                            else
                            {
                                MessageBox.Show("Сначала выделите строку сверху");
                            }
                            break;
                        case "Remove":
                            if(SelectString!=null)
                            {
                                if (SelectString.IsConsump == true)
                                {
                                    if (new QuestWin("Точно удалить расход?").ShowDialog() == true)
                                    {
                                        dbOp.Consumptions.Delete(SelectString.Id);
                                        OnPropertyChanged("SupplyStringsList");
                                    }
                                }
                                else MessageBox.Show("Нельзя отсюда удалять строки растворов");
                            }
                            else MessageBox.Show("Сначала выделите строку сверху"); 
                            break;
                    }



                }));
            }
        }


    }
}
