using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void LoadAll()
        {
            db.Solutions.ToList();
            db.Solution_Lines.ToList();
            db.Supplies.ToList();
            //db.Solution_Recipes.ToList();
            //db.Solution_Recipe_Lines.ToList();
        }

        public void DelSolutLines(int SolutId)
        {
            Solution Solut = db.Solutions.Find(SolutId);
            if(Solut.Solution_Lines!=null)
            {
                while(Solut.Solution_Lines.Count>0)
                {
                    db.Solution_Lines.Remove(Solut.Solution_Lines[0]);
                }
            }
        }
    }
}
