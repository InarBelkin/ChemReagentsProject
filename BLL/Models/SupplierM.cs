using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SupplierM : IModel<Supplier>
    {
        private string name;
        public string Name { get=>name; set { name = value;OnPropertyChanged(); } }

        internal override void setfromDal(Supplier item)
        {
            Id = item.Id;
            Name = item.Name;
        }

        internal override void updDal(Supplier item)
        {
            item.Id = Id;
            item.Name = Name;
        }
    }
}
