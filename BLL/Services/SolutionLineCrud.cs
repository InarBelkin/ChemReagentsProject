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
    public class SolutionLineCrud :ICrudRepos<SolutionLineM>
    {
        IDbRepos db;

        public SolutionLineCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(SolutionLineM item)
        {
            Solution_line s = item.getDal();
            db.Solution_Lines.Create(s);
            Save();
        }

        public void Delete(int id)
        {
            db.Solution_Lines.Delete(id);
            Save();
        }

        public SolutionLineM GetItem(int id)
        {
            var sl = new SolutionLineM(db.Solution_Lines.GetItem(id));
            sl.PropertyChanged += Sl_PropertyChanged;
            return sl;
        }

        public ObservableCollection<SolutionLineM> GetList()
        {
            ObservableCollection<SolutionLineM> ret = new ObservableCollection<SolutionLineM>();
            foreach(Solution_line l in db.Solution_Lines.GetList())
            {
                var sl = new SolutionLineM(l);
                sl.PropertyChanged += Sl_PropertyChanged;
                ret.Add(sl);
            }
            return ret;
        }

        public void Update(SolutionLineM item)
        {
            Solution_line l = db.Solution_Lines.GetItem(item.Id);
            item.updDal(l);
            db.Solution_Lines.Update(l);
            Save();
        }

        int Save()
        {
            return db.Save();
        }

        public ObservableCollection<SolutionLineM> SolutionLineBySolut(int SolutId)
        {
            ObservableCollection<SolutionLineM> ret = new ObservableCollection<SolutionLineM>();
            foreach (Solution_line s in db.Reports.SolutionLineBySolut(SolutId))
            {
                SolutionLineM sl = new SolutionLineM(s);
                sl.PropertyChanged += Sl_PropertyChanged;
                ret.Add(sl);
            }
            return ret;
        }

        private void Sl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if(e.PropertyName== "SelectReag")
            //{
            //    if(sender is SolutionLineM sl)
            //    {
            //        sl.SupplyList = new ObservableCollection<SupplyM>();
            //        var supList = db.Reports.SupplyByReag(sl.SelectReag.Id);
            //        foreach(Supply s in supList)
            //        {
            //            sl.SupplyList.Add(new SupplyM(s));
            //        }

            //    }
            //}
        }
    }
}
