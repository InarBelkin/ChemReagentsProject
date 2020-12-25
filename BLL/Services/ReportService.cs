using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Services
{
    public class ReportService : IReportServ
    {
        IDbRepos db;

        public ReportService(IDbRepos repos)
        {
            db = repos;
        }

        public ObservableCollection<SupplyM> SupplyByReag(int reagId)
        {
            ObservableCollection<SupplyM> ret = new ObservableCollection<SupplyM>();

            List<Supply> ss = db.Reports.SupplyByReag(reagId);

            bool ischange = false;
            foreach (Supply r in ss)
            {
                SupplyM s = new SupplyM(r);

                if (s.State == SupplStates.Active && DateTime.Now > s.Date_End.AddMonths(-1))
                {
                    s.State = SupplStates.SoonToWriteOff;
                    r.State = (byte)SupplStates.SoonToWriteOff;
                    db.Supplies.Update(r);
                    ischange = true;
                }
                if ((s.State == SupplStates.SoonToWriteOff || s.State == SupplStates.Active) && DateTime.Now > s.Date_End)
                {
                    s.State = SupplStates.ToWriteOff;
                    r.State = (byte)SupplStates.ToWriteOff;
                    db.Supplies.Update(r);
                    ischange = true;
                }


                if (ischange) db.Save();


                ret.Add(s);

            }
            return ret;
        }

        public ObservableCollection<SupplyM> SupplyByReagOnlyActual(int reagid, DateTime NowDate)
        {
            ObservableCollection<SupplyM> slist = SupplyByReag(reagid);
            ObservableCollection<SupplyM> ret = new ObservableCollection<SupplyM>();
            foreach (SupplyM s in slist)
            {
                if (s.Date_End.AddDays(1) > NowDate)
                {
                    ret.Add(s);
                }
            }
            return ret;
        }

        public void AcceptRecipe(int SolutId, DateTime NowDate)
        {
            db.Reports.AcceptRecipe(SolutId);   //удалили лишнее
            Solution solut = db.Solutions.GetItem(SolutId);
            if (solut.ConcentrationId != null)
            {
                Concentration conc = db.Concentrations.GetItem((int)solut.ConcentrationId);
                if (conc.LineList != null && conc.LineList.Count > 0)
                {
                    foreach (Solution_recipe_line srl in conc.LineList)
                    {
                        var listsuppl = SupplyByReagOnlyActual(srl.ReagentId, NowDate);
                        if (listsuppl.Count != 0)
                        {
                            db.Solution_Lines.Create(new Solution_line()
                            {
                                SolutionId = solut.Id,
                                NameOtherComponent = "",
                                SupplyId = listsuppl[0].Id,
                                Count = srl.Count,
                            });

                        }
                        else
                        {
                            MessageBox.Show("Для реактива " + srl.Reagent.Name + "не найдено активных поставок");
                        }
                    }

                }
            }
            db.Solutions.Update(solut);
            db.Save();
        }

        public ObservableCollection<ConcentrationM> ConcentrbyRecipe(int RecipeId)
        {
            ObservableCollection<ConcentrationM> ret = new ObservableCollection<ConcentrationM>();
            foreach (Concentration c in db.Reports.ConcentrbyRecipe(RecipeId))
            {
                ret.Add(new ConcentrationM(c));
            }
            return ret;
        }

        public ObservableCollection<SolutRezLineM> GetRecipeLine(int ConcentrId)
        {
            ObservableCollection<SolutRezLineM> ret = new ObservableCollection<SolutRezLineM>();

            foreach (Solution_recipe_line s in db.Reports.GetReciepeLine(ConcentrId))
            {
                SolutRezLineM sM = new SolutRezLineM(s);
                sM.PropertyChanged += SML_PropertyChanged;
                ret.Add(sM);
            }

            return ret;
        }

        private void SML_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ReagentId")
            {
                if (sender is SolutRezLineM s)
                {
                    Reagent r = db.Reagents.GetItem(s.ReagentId);
                    if (r != null)
                    {
                        s.Units = r.units;
                    }
                }
            }
        }


    }
}
