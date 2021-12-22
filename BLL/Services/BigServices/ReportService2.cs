using BLL.Models.OtherModels;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BigServices
{
    public partial class ReportService
    {
        public void AcceptWriteOff(MonthReportLineM m, ReportM r)
        {
            Supply s = db.Supplies.GetItem(m.SupplyId); //1
            if (m.AcceptWriteOff) //2
            {
                s.Active = false; //3
                s.Date_UnWrite = r.RealDate;
                s.ReportId = r.Id;
            }
            else
            {
                //4
                s.Active = true;
                s.Date_UnWrite = new DateTime(2100, 1, 1);
                s.ReportId = null;
            }

            db.Supplies.Update(s);  //5
            db.Save();  
        }

        public ReportM CreateMonthRep(ReportM rep)
        {
            Report r = new Report() { TimeRep = rep.TimeRep, RealDate = rep.RealDate };
            db.PReports.Create(r);
            return new ReportM(r);
        }

        public ReportM GetMonthRep(uint year, byte month)
        {
            ReportM ret = null;
            foreach (var r in db.PReports.GetList())
            {
                if (r.TimeRep.Year == year && r.TimeRep.Month == month)
                {
                    ret = new ReportM(r);
                    break;
                }
            }

            return ret;
        }
    }
}