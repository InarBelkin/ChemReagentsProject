using BLL.Interfaces;
using BLL.Models;
using ChemReagents.Additional;
using ChemReagents.Pages.DialogWins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        SupplyM TempSuppl;

        public SuppliesWinVM(IDBCrud cr, IReportServ report, SupplyM sup)
        {
            dbOp = cr;
            rep = report;
           
            SuppliersList = dbOp.Suppliers.GetList();
            if (sup.Id == 0)
            {
                TempSuppl = new SupplyM();
                TempSuppl.ReagentId = sup.ReagentId;
                TempSuppl.Density = sup.Density;
                TempSuppl.Date_Begin = DateTime.Today;
                TempSuppl.Date_End = DateTime.Today.AddYears(1);
            }
            else
            {
                TempSuppl = dbOp.Supplies.GetItem(sup.Id);
                foreach (SupplierM s in SuppliersList)
                {
                    if (s.Id == TempSuppl.SupplierId)
                    {
                        SelectSupplier = s; break;
                    }
                }
            }
            SelectReag = dbOp.Reagents.GetItem(sup.ReagentId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ReagentM selectReag;
        public ReagentM SelectReag { get => selectReag; set => selectReag = value; }
        public int SupplId => TempSuppl.Id;
        public int ReagId => TempSuppl.ReagentId;
        public int ReagNumb => SelectReag.Number;
        public string ReagName { get => SelectReag.Name; }
        //private ObservableCollection<ReagentM> reagentList;
        //public ObservableCollection<ReagentM> ReagentList { get => reagentList; set => reagentList = value; }
        public decimal Density { get => TempSuppl.Density; set => TempSuppl.Density = value; }
        private ObservableCollection<SupplierM> suppliersList;
        public ObservableCollection<SupplierM> SuppliersList { get => suppliersList; set => suppliersList = value; }
        private SupplierM selectSupplier;
        public SupplierM SelectSupplier { get => selectSupplier; set { selectSupplier = value; TempSuppl.SupplierId = value.Id; } }
        public DateTime DateBeg { get => TempSuppl.Date_Begin; set { TempSuppl.Date_Begin = value; OnPropertyChanged("DateBeg"); } }
        public DateTime DateEnd { get => TempSuppl.Date_End; set { TempSuppl.Date_End = value; OnPropertyChanged("DateEnd"); } }
        public decimal AmountGr { get => TempSuppl.CountMas; set { TempSuppl.CountMas = value; OnPropertyChanged("AmountGr"); OnPropertyChanged("AmountMl"); } }
        public decimal AmountMl { get => TempSuppl.CountVolum; set { TempSuppl.CountVolum = value; OnPropertyChanged("AmountMl"); OnPropertyChanged("AmountGr"); } }
        public bool Unpacked { get => TempSuppl.Unpacked; set => TempSuppl.Unpacked = value; }

        private RelayCommand saveButton;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveButton ?? (saveButton = new RelayCommand(obj =>
                {
                    switch(obj as string)
                    {
                        case "Save":
                            if (TempSuppl.Validate())
                            {
                                if(TempSuppl.Id==0)
                                {
                                    var ex = dbOp.Supplies.Create(TempSuppl);
                                    if (ex != null) new ErrorWin(ex).ShowDialog();
                                    else WindowService.CloseWindow(this, true);
                                }
                                else
                                {
                                    var ex = dbOp.Supplies.Update(TempSuppl);
                                    if (ex != null) new ErrorWin(ex).ShowDialog();
                                    else WindowService.CloseWindow(this, true);
                                }
                              
                            }
                            else new MyDialogWin("Что-то не довведено", false).ShowDialog();
                            break;
                        case "Cacnel":
                            WindowService.CloseWindow(this, false);

                            break;
                            
                    }
                }));
            }
        }
    }
}
