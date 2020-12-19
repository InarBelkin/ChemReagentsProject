using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.ViewModel
{
    class EditSuppliesVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        SupplyM EditSuppl;
        IEditSupplWin win;
        public EditSuppliesVM(IEditSupplWin interf,IDbCrud cr, IReportServ report, int SupplyId)
        {
            win = interf;
            dbOp = cr;
            rep = report;
            listReag = dbOp.Reagents.GetList();
            listSuppliers = dbOp.Suppliers.GetList();
            if (SupplyId == -2)
            {
                EditSuppl = new SupplyM()
                {
                    Id = -1,
                    Date_Begin = DateTime.Now,
                    Date_End = DateTime.Now,
                    Count = 0,
                };
            }
            else
            {
                EditSuppl = dbOp.Supplies.GetItem(SupplyId);
                Console.WriteLine();
                foreach (ReagentM reag in listReag)
                {
                    if(reag.Id == EditSuppl.ReagentId)
                    {
                        selectreag = reag;
                        break;
                    }
                }
                foreach(SupplierM sur in listSuppliers)
                {
                    if(sur.Id == EditSuppl.SupplierId )
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
            set => EditSuppl.Count = value;
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
                            if(EditSuppl.Id==-1)
                            {
                                dbOp.Supplies.Create(EditSuppl);
                            }
                            win.DialogRez(true);
                            break;
                        case "Cancel":
                            win.DialogRez(false);
                            break;
                        default:
                            break;
                    }

                  

                }));
            }
        }

    }
}
