using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.OtherModels;
using BLL.Services.FIlters;
using DAL.Interfaces;
using DAL.Tables;

namespace BLL.Services.BigServices
{
    public partial class ReportService : IReportServ
    {
        IDbRepos db;
        public ReportService(IDbRepos repos)
        {
            db = repos;
            // DAL.Additional.ExceptionSystemD.ConnectLost += ExceptionSystemD_ConnectLost;
        }

        public ObservableCollection<SolutionLineM> GetSuppliesForRecipe(int solutId, int recipeId, int concId, DateTime Date, decimal Count)
        {
            ObservableCollection<SolutionLineM> ret = new ObservableCollection<SolutionLineM>();
            Concentration conc = db.Concentrations.GetItem(concId);
            if (conc != null)
            {
                var conclist = conc.LineList;
                if (conclist != null)
                {
                    foreach (Solution_recipe_line l in conclist)
                    {
                        var SL = new SolutionLineM();
                        SL.ReagentId = l.ReagentId;
                        if (Count > 0 && l.Count > 0)
                            SL.Count = l.Count * (Count / conc.Count);
                        else SL.Count = 0;

                        if (l.Reagent.Supplies != null)
                        {
                            var SupplList = l.Reagent.Supplies.Where(i => Date >= i.Date_StartUse && Date <= i.Date_UnWrite.AddDays(1)).ToList();
                            SupplList.Sort(new SupplyDALComparer());
                            foreach (var sup in SupplList)
                            {
                                if (GetRemains(sup.Id, solutId) >= SL.Count)
                                {
                                    SL.SupplyId = sup.Id;
                                    SL.CountBalance = GetRemains(sup.Id, solutId);
                                    break;
                                }
                            }

                            if (SL.SupplyId == null && SupplList.Count > 0)
                            {
                                SL.SupplyId = SupplList[0].Id;
                                SL.CountBalance = GetRemains(SupplList[0].Id, solutId);
                            }
                        }
                        if (l.Reagent.isWater) SL.Units = "мл.";
                        else SL.Units = "гр.";

                        ret.Add(SL);
                    }
                }
            }
            else if (recipeId > 0)
            {
                Solution_recipe sr = db.Solution_Recipes.GetItem(recipeId);
                if (sr.Concentrations != null && sr.Concentrations.Count > 0)
                {
                    Concentration conc2 = sr.Concentrations[0];
                    if (conc2.LineList != null)
                    {
                        foreach (var l in conc2.LineList)
                        {
                            var SL = new SolutionLineM();
                            SL.Count = 0;
                            SL.ReagentId = l.ReagentId;
                            if (l.Reagent.Supplies != null)
                            {
                                var SupplList = l.Reagent.Supplies.Where(i => Date >= i.Date_StartUse && Date <= i.Date_UnWrite.AddDays(1)).ToList();
                                SupplList.Sort(new SupplyDALComparer());
                                if (SupplList.Count > 0)
                                {
                                    SL.SupplyId = SupplList[0].Id;
                                    SL.CountBalance = GetRemains(SupplList[0].Id, solutId);
                                }
                            }
                            if (l.Reagent.isWater) SL.Units = "мл.";
                            else SL.Units = "гр.";
                            ret.Add(SL);
                        }
                    }
                }
            }
            return ret;
        }

        public decimal GetRemains(int supplId, int SolutionId = 0)  //дописать: исключить какой-то раствор, добавить учёт отдельных списаний
        {
            Supply Sup = db.Supplies.GetItem(supplId);
            if (Sup.Reagent.IsAccounted)
            {
                bool isWater = Sup.Reagent.isWater;
                decimal summ = 0;
                if (Sup.Solution_Lines != null)
                {
                    foreach (var SL in Sup.Solution_Lines)
                    {
                        if (SL.SolutionId != SolutionId)
                        {
                            summ += SL.Count;
                        }
                    }
                }
                if (Sup.Consumptions != null)
                {
                    foreach (var sc in Sup.Consumptions)
                    {
                        summ += sc.Count;
                    }
                }
                decimal remain = 0;
                if (isWater) { remain = Sup.Count / Sup.Density - summ; }
                else
                    remain = Sup.Count - summ;
                return remain;
            }
            else
            {
                return 9999;
            }

        }

        public (decimal mas, decimal vol) GetRemainsSW(int suplid, DateTime dateEnd, decimal Count, decimal Density, bool OnDate = false)
        {
            Supply Sup = db.Supplies.GetItem(suplid);
            bool isWater = Sup.Reagent.isWater;
            decimal summ = 0;
            if (Sup.Solution_Lines != null)
            {
                foreach (var SL in Sup.Solution_Lines)
                {
                    if (!OnDate || SL.Solution.Date_Begin <= dateEnd)
                    {
                        summ += SL.Count;
                    }
                }
            }
            if (Sup.Consumptions != null)
            {
                foreach (var sc in Sup.Consumptions)
                {
                    if (!OnDate || sc.DateBegin <= dateEnd)
                        summ += sc.Count;
                }
            }
            (decimal, decimal) remain = (0, 0);
            if (isWater)
            {
                remain.Item1 = Count - (summ * Density);
                remain.Item2 = Count / Density - summ;
            }
            else
            {
                remain.Item1 = Count - summ;
            }
            return remain;
        }

        public void LoadAll()
        {
            db.Reports.LoadAll();
        }

        public void DeleteSolutLines(int SolutId)
        {
            db.Reports.DelSolutLines(SolutId);
        }

        public MonthM[] GetMonths()
        {
            MonthM[] ret = new MonthM[12];
            ret[0] = new MonthM() { num = 1, Name = "Январь" };
            ret[1] = new MonthM() { num = 2, Name = "Февраль" };
            ret[2] = new MonthM() { num = 3, Name = "Март" };
            ret[3] = new MonthM() { num = 4, Name = "Апрель" };
            ret[4] = new MonthM() { num = 5, Name = "Май" };
            ret[5] = new MonthM() { num = 6, Name = "Июнь" };
            ret[6] = new MonthM() { num = 7, Name = "Июль" };
            ret[7] = new MonthM() { num = 8, Name = "Август" };
            ret[8] = new MonthM() { num = 9, Name = "Сентябрь" };
            ret[9] = new MonthM() { num = 10, Name = "Октябрь" };
            ret[10] = new MonthM() { num = 11, Name = "Ноябрь" };
            ret[11] = new MonthM() { num = 12, Name = "Декабрь" };
            return ret;
        }


        public (List<MonthReportLineM>, List<MonthReportLineM>) GetListReport(uint year, byte month)
        {
            List<MonthReportLineM> Ostatk = new List<MonthReportLineM>();
            List<MonthReportLineM> Write = new List<MonthReportLineM>();
            foreach (var s in db.Supplies.GetList())
            {
                if (s.Reagent.IsAccounted)
                {
                    MonthReportLineM r = null;
                    Report rep = null;
                    if (s.ReportId != null) rep = db.PReports.GetItem((int)s.ReportId);
                    if ((s.ReportId == null && s.Date_Expiration < new DateTime((int)year, month + 1, 1)) || (s.ReportId!=null && rep.TimeRep.Year == year && rep.TimeRep.Month == month))
                    {
                        r = new MonthReportLineM();
                        Write.Add(r);
                    }
                    else 
                    {
                        decimal cons = GetConsSupplMonth(s.Id, year, month);
                        if(cons>0)
                        {
                            r = new MonthReportLineM();
                            Ostatk.Add(r);
                        }
                    }

                    if(r!=null)
                    {
                        r.SupplyId = s.Id;
                        r.ReagentName = s.Reagent.Name;
                        r.CountMonth = GetConsSupplMonth(s.Id, year, month);

                        (decimal, decimal) remain = GetRemainsSW(s.Id, new DateTime((int)year, month, DateTime.DaysInMonth((int)year, month)), s.Count, s.Density, true);
                        r.RemainMonth = s.Reagent.isAlwaysWater ? remain.Item2 : remain.Item1;
                        r.Units = s.Reagent.isAlwaysWater ? "мл." : "гр.";
                        r.IsWrittenOff = s.ReportId == null ? false : true;
                        r.AcceptWriteOff = r.IsWrittenOff;
                    }

                }
            }
            return (Ostatk, Write);



        }

        public decimal GetConsSupplMonth(int SupplId, uint year, byte month)
        {
            decimal rem = 0;
            Supply s = db.Supplies.GetItem(SupplId);
            if (s.Solution_Lines != null)
            {
                foreach (var sl in s.Solution_Lines)
                {
                    if (sl.Solution.Date_Begin.Year == year && sl.Solution.Date_Begin.Month == month)
                    {
                        rem += sl.Count;
                    }
                }
            }
            if (s.Consumptions != null)
            {
                foreach (var c in s.Consumptions)
                {
                    if (c.DateBegin.Year == year && c.DateBegin.Month == month)
                    {
                        rem += c.Count;
                    }
                }
            }
            if (s.Reagent.isWater == false) return rem; //твёрдое
            else if (s.Reagent.isAlwaysWater)    //всегда жидкость
            {
                return rem;
            }
            else                //просто жидкость, надо граммы.
            {
                return rem * s.Density;
            }
        }
    }

}
