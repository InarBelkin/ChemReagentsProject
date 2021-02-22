using BLL.Interfaces;
using BLL.Models;
using BLL.Services.FIlters;
using ChemReagents.Additional;
using ChemReagents.AdditionalWins.SettingsWin;
using ChemReagents.Pages.DialogWins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                TempSuppl.DateProduction = DateTime.Today;
                TempSuppl.DateStartUse = DateTime.Today;
                TempSuppl.DateExpiration = DateTime.Today.AddYears(1);
                TempSuppl.DateUnWrite = new DateTime(2100, 1, 1);
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
        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var s in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
            }
        }

        public GridLength DevModeHeigh
        {
            get
            {
                if (Settings.DevMode)
                {
                    return GridLength.Auto;
                }
                else return new GridLength(0);
            }
        }

        public GridLength IsWaterHeigh
        {
            get
            {
                if (SelectReag.IsWater)
                {
                    return GridLength.Auto;
                }
                else return new GridLength(0);
            }
        }

        public GridLength UnWriteHeigh
        {
            get
            {
                if (TempSuppl.Active)
                {
                    return GridLength.Auto;
                }
                else return new GridLength(0);
            }
        }

        private ReagentM selectReag;
        public ReagentM SelectReag { get => selectReag; set => selectReag = value; }
        public int SupplId => TempSuppl.Id;
        public int ReagId => TempSuppl.ReagentId;
        public int ReagNumb => SelectReag.Number;
        public string ReagName { get => SelectReag.Name; }
        //private ObservableCollection<ReagentM> reagentList;
        //public ObservableCollection<ReagentM> ReagentList { get => reagentList; set => reagentList = value; }
        private ObservableCollection<SupplierM> suppliersList;
        public ObservableCollection<SupplierM> SuppliersList { get => suppliersList; set => suppliersList = value; }
        private SupplierM selectSupplier;
        public SupplierM SelectSupplier { get => selectSupplier; set { selectSupplier = value; TempSuppl.SupplierId = value.Id; } }
        //public DateTime DateBeg { get => TempSuppl.Date_Begin; set { TempSuppl.Date_Begin = value; OnPropertyChanged("DateBeg"); } }
        //public DateTime DateEnd { get => TempSuppl.Date_End; set { TempSuppl.Date_End = value; OnPropertyChanged("DateEnd"); } }
        public DateTime DateProduction { get => TempSuppl.DateProduction; set { TempSuppl.DateProduction = value; OnPropertyChanged("DateProduction"); } }
        public DateTime DateStartUse { get => TempSuppl.DateStartUse; set { TempSuppl.DateStartUse = value; OnPropertyChanged("DateStartUse"); } }
        public DateTime DateExpiration { get => TempSuppl.DateExpiration; set { TempSuppl.DateExpiration = value; OnPropertyChanged("DateExpiration"); } }
        public DateTime DateUnWrite { get => TempSuppl.DateUnWrite; set { TempSuppl.DateUnWrite = value; OnPropertyChanged("DateUnWrite"); } }

        public decimal Density { get => TempSuppl.Density; set { TempSuppl.Density = value;OnPropertyChanged("Density","AmountGr", "AmountMl"); } }

        public uint AmountGr { get => (uint)TempSuppl.CountMas; set { TempSuppl.CountMas = value; OnPropertyChanged("AmountGr"); OnPropertyChanged("AmountMl"); } }
        public uint AmountMl { get => (uint)TempSuppl.CountVolum; set { TempSuppl.CountVolum = value; OnPropertyChanged("AmountMl"); OnPropertyChanged("AmountGr"); } }

        public decimal RemanGr;
        public decimal RemainMl;

        private RelayCommand saveButton;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveButton ?? (saveButton = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Save":
                            if (TempSuppl.Validate())
                            {
                                if (TempSuppl.Id == 0)
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

        public ObservableCollection<SupplyStingM> ConsumpList
        {
            get
            {
                if (TempSuppl.Id > 0)
                {
                    var ret = dbOp.SupplConsump.GetList(new SupplyStringsFilter() { SupplyId = TempSuppl.Id });
                    return ret;
                }
                else return new ObservableCollection<SupplyStingM>();
            }
        }

        private SupplyStingM selectSupstr;
        public SupplyStingM SelectSupString
        {
            get => selectSupstr;
            set
            {
                selectSupstr = value;
                OnPropertyChanged("SelectSupString");
            }
        }

        private RelayCommand editConsump;
        public RelayCommand EditConsump
        {
            get
            {
                return editConsump ?? (editConsump = new RelayCommand(obj =>
                {
                    if (TempSuppl.Id > 0)
                    {
                        switch (obj as string)
                        {
                            case "Add":
                                var adv = new SupplyStingM() { SupplyId = TempSuppl.Id, DateBegin = DateTime.Now };
                                if (new WinConsump(dbOp, rep, adv).ShowDialog() == true)
                                {
                                    var ex = dbOp.SupplConsump.Create(adv);
                                    if (ex != null) new ErrorWin(ex).ShowDialog();
                                }
                                break;
                            case "Edit":
                                if (SelectSupString.IsConsump)
                                {
                                    if (new WinConsump(dbOp, rep, selectSupstr).ShowDialog() == true)
                                    {
                                        var ex = dbOp.SupplConsump.Update(selectSupstr);
                                        if (ex != null) new ErrorWin(ex).ShowDialog();
                                    }
                                }
                                else new MyDialogWin("Это не отдельное списание, его изменить отсюда нельзя", false).ShowDialog();
                                break;
                            case "Delete":
                                if (SelectSupString.IsConsump)
                                {
                                    dbOp.SupplConsump.Delete(SelectSupString.Id);
                                }
                                else new MyDialogWin("Это не отдельное списание, его удалить отсюда нельзя", false).ShowDialog();
                                break;
                        }
                    }
                }));
            }
        }
    }
}
