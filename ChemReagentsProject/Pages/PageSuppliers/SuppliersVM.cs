using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.Pages.PageSuppliers
{
    class SuppliersVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;

        public SuppliersVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<SupplierM> suppliersList;

        public ObservableCollection<SupplierM> SuppliersList
        {
            get
            {
                suppliersList = dbOp.Suppliers.GetList();
                foreach(var sr in suppliersList)
                {
                    sr.PropertyChanged += Supplier_PropertyChanged;
                }
                suppliersList.CollectionChanged += SupplierList_CollectionChanged;
                return suppliersList;
            }
            set { suppliersList = value; OnPropertyChanged("SuppliersList"); }
        }

         

        private void Supplier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var ex = dbOp.Suppliers.Update(sender as SupplierM);
            if (ex != null) MessageBox.Show(ex.Message);
        }

        private void SupplierList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    SupplierM suplr = new SupplierM() { Name = "" };
                    dbOp.Suppliers.Create(suplr);
                    OnPropertyChanged("SuppliersList");
                    break;

            }
        }

        private SupplierM selectSuplr;
        public SupplierM SelectSuplr
        {
            get => selectSuplr;
            set
            {
                if(value is SupplierM sr)
                {
                    if(sr.Id!=0)
                    {
                        selectSuplr = sr;
                    }
                }
            }
        }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    SupplierM suplr = new SupplierM() { Name = "" };
                    dbOp.Suppliers.Create(suplr);
                    OnPropertyChanged("SuppliersList");
                }));
            }
        }
    }
}
