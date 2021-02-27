using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.OtherModels
{
    public  class ReportM : IModel<Report>
    {
        public int Id { get; set; }
        public DateTime TimeRep { get; set; }

        public ReportM() { }
        public ReportM(Report r) { setfromDal(r); }

        internal override void setfromDal(Report item)
        {
            Id = item.Id;
            TimeRep = item.TimeRep;
        }

        internal override void updDal(Report item)
        {
            item.Id = Id;
            item.TimeRep = TimeRep;
        }
    }
}
