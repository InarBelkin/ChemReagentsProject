using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SupplierM :IModel<Supplier>
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
    }

}
