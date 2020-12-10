using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SupplierM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SupplierM() { }
        public SupplierM(Supplier s)
        {
            Id = s.Id;
            Name = s.Name;
        }
    }
}
