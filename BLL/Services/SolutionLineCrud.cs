using BLL.Additional;
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

namespace BLL.Services
{
    public class SolutionLineCrud :ICrudRepos<SolutionLineM>
    {
        IDbRepos db;

        public SolutionLineCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public Exception Create(SolutionLineM item)
        {
            Solution_line s = item.getDal();
            db.Solution_Lines.Create(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                db.Solution_Lines.Delete(s.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании строки раствора произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать строку раствора", ex1);
            }
            else return null;
        }

        public void Delete(int id)
        {
            db.Solution_Lines.Delete(id);
            Save();
        }

        public SolutionLineM GetItem(int id)
        {
            var sl = new SolutionLineM(db.Solution_Lines.GetItem(id));
           
            return sl;
        }

        public ObservableCollection<SolutionLineM> GetList()
        {
            ObservableCollection<SolutionLineM> ret = new ObservableCollection<SolutionLineM>();
            foreach(Solution_line l in db.Solution_Lines.GetList())
            {
                var sl = new SolutionLineM(l);
             
                ret.Add(sl);
            }
            return ret;
        }

        public Exception Update(SolutionLineM item)
        {
            Solution_line l = db.Solution_Lines.GetItem(item.Id);
            SolutionLineM back = new SolutionLineM(l);
            item.updDal(l);
            db.Solution_Lines.Update(l);
            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(l);
                db.Solution_Lines.Update(l);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении строки раствора произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить строку раствора", ex1);
            }
            return null;
        }

        public Exception Save()
        {
            return db.Save();

        }

        public ObservableCollection<SolutionLineM> SolutionLineBySolut(int SolutId)
        {
            ObservableCollection<SolutionLineM> ret = new ObservableCollection<SolutionLineM>();
            foreach (Solution_line s in db.Reports.SolutionLineBySolut(SolutId))
            {
                SolutionLineM sl = new SolutionLineM(s);
                ret.Add(sl);
            }
            return ret;
        }


    }
}
