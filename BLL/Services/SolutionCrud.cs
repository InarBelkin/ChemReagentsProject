﻿using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class SolutionCrud : ICrudRepos<SolutionM>
    {
        IDbRepos db;

        public SolutionCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(SolutionM item)
        {
            Solution s = item.getDal();
            db.Solutions.Create(s);
            Save();
        }

        public void Delete(int id)
        {
            db.Solutions.Delete(id);
            Save();
        }

        public SolutionM GetItem(int id)
        {
            var s = new SolutionM(db.Solutions.GetItem(id));
            if(s.ConcentrationId!=null)
            {
                Concentration c = db.Concentrations.GetItem((int)s.ConcentrationId);
                s.SolutionRecipeId = db.Solution_Recipes.GetItem(c.SolutionRecipeId).Id;
            }
            return s;
        }

     
        public ObservableCollection<SolutionM> GetList()
        {
            ObservableCollection<SolutionM> ret = new ObservableCollection<SolutionM>();
            foreach (Solution ss in db.Solutions.GetList())
            {
                var s = new SolutionM(ss);
                if (s.ConcentrationId != null)
                {
                    Concentration c = db.Concentrations.GetItem((int)s.ConcentrationId);
                    s.SolutionRecipeId = db.Solution_Recipes.GetItem(c.SolutionRecipeId).Id;
                }
                ret.Add(s);
            }
            return ret;
        }



        public void Update(SolutionM item)
        {
            
            Solution s = db.Solutions.GetItem(item.Id);
            if(s.ConcentrationId ==9 && item.ConcentrationId==2)
            {

            }
            item.updDal(s);
            db.Solutions.Update(s);
            Save();
        }

        private int Save()
        {
            return db.Save();
        }
    }
}
