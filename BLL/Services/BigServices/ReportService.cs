using BLL.Additional;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.OtherModels;
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

            // bool ischange = false;
            foreach (Supply r in ss)
            {
                SupplyM s = new SupplyM(r);

                //if (s.State == SupplStates.Active && DateTime.Now > s.Date_End.AddMonths(-1))
                //{
                //    s.State = SupplStates.SoonToWriteOff;
                //    r.State = (byte)SupplStates.SoonToWriteOff;
                //    db.Supplies.Update(r);
                //    ischange = true;
                //}
                //if ((s.State == SupplStates.SoonToWriteOff || s.State == SupplStates.Active) && DateTime.Now > s.Date_End)
                //{
                //    s.State = SupplStates.ToWriteOff;
                //    r.State = (byte)SupplStates.ToWriteOff;
                //    db.Supplies.Update(r);
                //    ischange = true;
                //}


                //if (ischange) db.Save();


                ret.Add(s);

            }
            return ret;
        }



        /// <summary>
        /// Получает все поставки для этого реактива, что ещё не протухли на заданный момент и уже созданы
        /// </summary>
        public ObservableCollection<SupplyM> SupplyByReagOnlyActual(int reagid, DateTime NowDate)   //тоже бы отсортировать
        {
            ObservableCollection<SupplyM> slist = SupplyByReag(reagid);
            List<SupplyM> ret = new List<SupplyM>();
            foreach (SupplyM s in slist)
            {
                if (s.Date_End.AddDays(1) > NowDate)
                {
                    ret.Add(s);
                }
            }
            ret.Sort( new SupplDateComparer());


            ObservableCollection<SupplyM> rets = new ObservableCollection<SupplyM>(ret);
            return rets;
        }

        /// <summary>
        /// Удаляет старое содержание этого раствора и устанавливает новый рецепт(id рецепта-концентрации уже должно быть в нём)
        /// </summary>
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
                        if (listsuppl.Count != 0)                           //Надо учесть ещё и оставшееся там количество
                        {
                            bool b = false;
                            foreach(SupplyM sup in listsuppl)
                            {
                                if(sup.Count-GetSupplyStrings(sup.Id).Summ <=0)
                                {
                                    continue;
                                }
                                else if(sup.Count-GetSupplyStrings(sup.Id).Summ-srl.Count<0)
                                {
                                    db.Solution_Lines.Create(new Solution_line()
                                    {
                                        SolutionId = solut.Id,
                                        NameOtherComponent = "",
                                        SupplyId = sup.Id,
                                        Count = sup.Count - GetSupplyStrings(sup.Id).Summ,
                                    });
                                    b = true;
                                    MessageBox.Show("Для реактива " + srl.Reagent.Name + " не хватает количества в самом старом реактиве\nДобавлено столько, сколько было");
                                    break;
                                }
                                else
                                {
                                    db.Solution_Lines.Create(new Solution_line()
                                    {
                                        SolutionId = solut.Id,
                                        NameOtherComponent = "",
                                        SupplyId = sup.Id,
                                        Count =srl.Count,
                                    });
                                    b = true;
                                    break;
                                }
                            }
                            if(!b) MessageBox.Show("Для реактива " + srl.Reagent.Name + "нет поставок с количеством>0");
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

        /// <summary>
        /// Получает все расходы этого реактива и сумму этих расходов
        /// </summary>
        public (List<SupplyStringM>, float Summ) GetSupplyStrings(int SupplId)  //ещё сортировку надо
        {
            List<SupplyStringM> ret = new List<SupplyStringM>();
            db.Solutions.GetList();
            db.Concentrations.GetList();
            db.Solution_Recipes.GetList();
            Supply s = db.Supplies.GetItem(SupplId);
            if (s == null) return (null, 0);
            if (s.Solution_Lines != null)
            {
                foreach (Solution_line sl in s.Solution_Lines)
                {
                    SupplyStringM a = new SupplyStringM(sl);
                    ret.Add(a);
                }
            }
            if (s.Consumptions != null)
            {
                foreach (Supply_consumption cons in s.Consumptions)
                {
                    SupplyStringM b = new SupplyStringM(cons);
                    ret.Add(b);
                }
            }

            float summ = 0;
            foreach (SupplyStringM str in ret)
            {
                summ += str.Count;
            }

            return (ret, summ);
        }

        public List<MonthReportM> GetMonthReport(DateTime start, DateTime end)
        {
            List<MonthReportM> ret = new List<MonthReportM>();

            var supplies = db.Supplies.GetList();

            foreach (Supply s in supplies)
            {
                if (s.State != (byte)SupplStates.WriteOff)  //состояние не должно быть списанное
                {
                    var getstr = GetSupplyStrings(s.Id);
                    if (s.Date_End < end ) //если протух или количество==0, списываем
                    {
                        float sum = 0;
                        foreach (SupplyStringM ss in getstr.Item1)
                        {
                            if (ss.DateUse < start)
                            {
                                sum += ss.Count;
                            }

                        }
                        MonthReportM a = new MonthReportM()
                        {
                            Count = s.count - sum,
                            SupplyId = s.Id,
                            ReagentName = s.Reagent.Name,
                            status = "Списание по сроку годности",
                        };
                        ret.Add(a);

                    }
                    else
                    {
                        if (s.count - getstr.Summ <= 0)
                        {
                            float sum = 0;
                            foreach (SupplyStringM ss in getstr.Item1)
                            {
                                if (ss.DateUse < start)
                                {
                                    sum += ss.Count;
                                }

                            }
                            MonthReportM a = new MonthReportM()
                            {
                                Count = s.count - sum,
                                SupplyId = s.Id,
                                ReagentName = s.Reagent.Name,
                                status = "Списание из-за количества",
                            };
                            ret.Add(a);
                        }
                        else
                        {
                            float sum = 0;
                            foreach (SupplyStringM ss in getstr.Item1)
                            {
                                if (ss.DateUse > start && ss.DateUse < end)
                                {
                                    sum += ss.Count;
                                }
                            }
                            if (sum > 0)
                            {
                                MonthReportM a = new MonthReportM()
                                {
                                    Count = sum,
                                    SupplyId = s.Id,
                                    ReagentName = s.Reagent.Name,
                                    status = "Расход",
                                };
                                ret.Add(a);
                            }
                        }


                    }


                }
            }


            return ret;
        }


    }
}
