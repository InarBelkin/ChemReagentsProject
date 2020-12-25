using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class Supply_consumption
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; }
        public string Name { get; set; }
        public float Count { get; set; }

    }
}
