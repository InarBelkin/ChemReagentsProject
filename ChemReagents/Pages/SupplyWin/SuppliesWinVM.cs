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
            OnDatePick = DateTime.Today;
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
                if (!TempSuppl.Active)
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
       
        public string IncomContr { get => TempSuppl.IncomContr; set => TempSuppl.IncomContr = value; }
        public string Qualification { get => TempSuppl.Qualification; set => TempSuppl.Qualification=value; }
        public string Manufacturer { get => TempSuppl.Manufacturer; set => TempSuppl.Manufacturer = value; }
        public DateTime DateProduction { get => TempSuppl.DateProduction; set { TempSuppl.DateProduction = value; OnPropertyChanged("DateProduction"); } }
        private ObservableCollection<SupplierM> suppliersList;
        public ObservableCollection<SupplierM> SuppliersList { get => suppliersList; set => suppliersList = value; }
        private SupplierM selectSupplier;
        public SupplierM SelectSupplier { get => selectSupplier; set { selectSupplier = value; TempSuppl.SupplierId = value.Id; } }
        public DateTime DateStartUse { get => TempSuppl.DateStartUse; set { TempSuppl.DateStartUse = value; OnPropertyChanged("DateStartUse"); } }
        public DateTime DateExpiration { get => TempSuppl.DateExpiration; set { TempSuppl.DateExpiration = value; OnPropertyChanged("DateExpiration"); } }
        public DateTime DateUnWrite { get => TempSuppl.DateUnWrite; set { TempSuppl.DateUnWrite = value; OnPropertyChanged("DateUnWrite"); } }

        public decimal Density
        {
            get => TempSuppl.Density;
            set
            {
                if (SelectReag.IsWater)
                {
                    decimal TAmountMl = AmountMl;
                    TempSuppl.Density = value;
                    TempSuppl.CountVolum = TAmountMl;
                }
                else
                {
                    TempSuppl.Density = value;
                }
                // TempSuppl.Density = value;

                OnPropertyChanged("Density", "AmountGr", "AmountMl", "RemaindGr", "RemaindMl");
            }
        }

        public uint AmountGr { get => (uint)TempSuppl.CountMas; set { TempSuppl.CountMas = value; OnPropertyChanged("AmountGr"); OnPropertyChanged("AmountMl", "RemaindGr", "RemaindMl"); } }
        public uint AmountMl { get => (uint)TempSuppl.CountVolum; set { TempSuppl.CountVolum = value; OnPropertyChanged("AmountMl"); OnPropertyChanged("AmountGr", "RemaindGr", "RemaindMl"); } }

        //public decimal RemaindGr { get { return rep.GetRemains(TempSuppl.Id, OnDatePick, TurnOnDate); } }
        public decimal RemaindGr { get { return TempSuppl.Id>0 ? rep.GetRemainsSW(TempSuppl.Id, OnDatePick, TempSuppl.CountMas, TempSuppl.Density,TurnOnDate).mas:0; } }
        public decimal RemaindMl { get => TempSuppl.Id>0? rep.GetRemainsSW(TempSuppl.Id, OnDatePick, TempSuppl.CountMas,TempSuppl.Density, TurnOnDate).vol:0; }

        private bool turnOnDate;
        public bool TurnOnDate
        {
            get => turnOnDate;
            set { turnOnDate = value; OnPropertyChanged("TurnOnDate", "RemaindGr", "RemaindMl", "ConsumpList"); }
        }

        private DateTime onDatePick;
        public DateTime OnDatePick { get => onDatePick; set { onDatePick = value; OnPropertyChanged("OnDatePick", "RemaindGr", "RemaindMl", "ConsumpList"); } }
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
                    SupplyStringsFilter f = new SupplyStringsFilter() { SupplyId = TempSuppl.Id };
                    if (TurnOnDate) f.DateTo = OnDatePick;
                    var ret = dbOp.SupplConsump.GetList(f);
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
