using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Tables;

namespace DAL.Repository
{
    public class ReportsRepSQL : IReportRepos
    {
        private ChemContext db;

        public ReportsRepSQL(ChemContext dbcontext)
        {
            db = dbcontext;
        }
        public List<Supply> SupplyByReag(int reagId)
        {
            // Reagent d = db.Reagents.Find(reagId);
            Reagent r = db.Reagents.Find(reagId);
            List<Supply> a = null;
            if (r != null)
            {
                a = r.Supplies;
            }
            return a ?? new List<Supply>();
        }

        public List<Solution_recipe_line> GetReciepeLine(int ConcentrId)
        {
            Concentration c = db.Concentration.Find(ConcentrId);
            List<Solution_recipe_line> a = null;
            if (c != null)
            {
                a = c.LineList;
            }

            return a ?? new List<Solution_recipe_line>();
        }

        public List<Concentration> ConcentrbyRecipe(int RecipeId)
        {
            Solution_recipe s = db.Solution_Recipes.Find(RecipeId);
            List<Concentration> a = null;
            if (s != null)
            {
                a = s.Concentrations;
            }
            return a ?? new List<Concentration>();
        }

        public List<Solution_line> SolutionLineBySolut(int SolutId)
        {
            Solution s = db.Solutions.Find(SolutId);
            List<Solution_line> a = null;
            if (s != null)
            {
                a = s.Solution_Lines;
            }
            return a ?? new List<Solution_line>();
        }

        /// <summary>
        /// Удаляет строки рецепта для этого рецепта
        /// </summary>
        public bool AcceptRecipe(int SolutionId)
        {
            Solution solut = db.Solutions.Find(SolutionId);

            if (solut != null && solut.Solution_Lines != null)
            {
                while(solut.Solution_Lines.Count>0)
                {
                    db.Solution_Lines.Remove(solut.Solution_Lines[0]);
                }
                //foreach (Solution_line sl in solut.Solution_Lines)
                //{
                //    db.Solution_Lines.Remove(sl);
                //}
                // db.SaveChanges();//надо сохранить уже в bll

            }
            else return false; //свою часть мы выполнили, теперь надо   

            //if (solut.ConcentrationId != null)      //если сменили на 0, рецепт очищен, всё готово
            //{
            //    Concentration contr = db.Concentration.Find(solut.ConcentrationId); //
            //    if (contr != null && contr.LineList != null)
            //    {
            //        foreach (Solution_recipe_line srl in contr.LineList)
            //        {
            //            db.Solution_Lines.Add(new Solution_line
            //            {
            //                SolutionId = solut.Id,


            //            });
            //        }
            //    }
            //}
            //else return true;
            
           



            return true;
        }
    }
}
