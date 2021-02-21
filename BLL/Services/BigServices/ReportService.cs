using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services.FIlters;
using DAL.Interfaces;
using DAL.Tables;

namespace BLL.Services.BigServices
{
    public class ReportService : IReportServ
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
                if(conclist!=null)
                {
                    foreach (Solution_recipe_line l in conclist)
                    {
                        var SL = new SolutionLineM();
                        SL.ReagentId = l.ReagentId;
                        if (Count > 0 && l.Count > 0)
                            SL.Count = l.Count * (Count / conc.Count);
                        else SL.Count = 0;

                        if(l.Reagent.Supplies!=null)
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
                       
                        ret.Add(SL);
                    }
                }
            }
            else if(recipeId>0)
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
                            if(l.Reagent.Supplies!=null)
                            {
                                var SupplList = l.Reagent.Supplies.Where(i => Date >= i.Date_StartUse && Date <= i.Date_UnWrite.AddDays(1)).ToList();
                                SupplList.Sort(new SupplyDALComparer());
                                if(SupplList.Count>0)
                                {
                                    SL.SupplyId = SupplList[0].Id;
                                    SL.CountBalance = GetRemains(SupplList[0].Id, solutId);
                                }
                            }
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
            if(Sup.Consumptions!=null)
            {
                foreach(var sc in Sup.Consumptions)
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

        public void LoadAll()
        {
            db.Reports.LoadAll();
        }

        public void DeleteSolutLines(int SolutId)
        {
            db.Reports.DelSolutLines(SolutId);
        }
    }

}
