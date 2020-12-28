using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SupplierM :IModel<Supplier> , INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        public string Name { get =>name; set { name = value;OnPropertyChanged(); } }

        public SupplierM() { }
        public SupplierM(Supplier s)
        {
            Id = s.Id;
            Name = s.Name;
        }

        public Supplier getDal()
        {
            Supplier s = new Supplier();
            s.Id = Id;
            s.Name = Name;
            return s;
        }

        public void updDal(Supplier s)
        {
            s.Id = Id;
            s.Name = Name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
