using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.PageSolutions
{
    class SolutionVM : IRecognizable, INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        IPageSolution page;
        public bool close;
        public SolutionVM(IDbCrud cr, IReportServ report, IPageSolution pg)
        {
            dbOp = cr;
            rep = report;
            page = pg;
            InarService.ChangeSelectRez += InarService_ChangeSelectRez;
            InarService.ChangeSelectConcentr += InarService_ChangeSelectConcentr;
            close = false;
            InarService.closesolutEv += InarService_closesolutEv;

        }

        private void InarService_closesolutEv(object sender, bool e)
        {
            close = true;
        }

        private void InarService_ChangeSelectRez(object sender, SolutionRezipeM e)
        {
            if (selectSolution != null)
            {
                if (e != null)
                {
                    var a = rep.ConcentrbyRecipe(e.Id);
                    selectSolution.ConcentList = a;
                    selectSolution.SolutionRecipeId = e.Id;
                }

            }
        }

        private void InarService_ChangeSelectConcentr(object sender, ConcentrationM e)
        {
            if (selectSolution != null && e != null)
            {
                selectSolution.ConcentrationId = e.Id;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid GetGuid()
        {
            throw new NotImplementedException();
        }

        private ObservableCollection<SolutionM> solutionList;
        public ObservableCollection<SolutionM> SolutionList
        {
            get
            {
                solutionList = dbOp.Solutions.GetList();
                var recipelist = dbOp.SolutRecipes.GetList();
                foreach (SolutionM s in solutionList)
                {

                    s.RecipeList = recipelist;
                    if (s.ConcentrationId != null)
                    {
                        int recid = dbOp.Concentrations.GetItem((int)s.ConcentrationId).SolutionRecipeId;
                        foreach (SolutionRezipeM r in recipelist)
                        {
                            if (r.Id == recid)
                            {
                                s.SelectRecipe = r;
                                s.SolutionRecipeId = recid;
                                var a = rep.ConcentrbyRecipe(recid);
                                s.ConcentList = a;
                                foreach (ConcentrationM c in s.ConcentList)
                                {
                                    if (c.Id == s.ConcentrationId)
                                    {
                                        s.Selectconcent = c;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    s.PropertyChanged += Solution_PropertyChanged;
                }
                solutionList.CollectionChanged += SolutionList_CollectionChanged;
                var solutrec = dbOp.SolutRecipes.GetList();

                return solutionList;
            }
        }

        private SolutionM selectSolution;
        public SolutionM SelectSolution
        {
            get => selectSolution;
            set
            {
                selectSolution = value;
            }
        }


        private void SolutionList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SolutionM s = new SolutionM() { Date_Begin = DateTime.Now };
                    dbOp.Solutions.Create(s);
                    OnPropertyChanged("SolutionList");
                    break;
            }
        }

        private void Solution_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!close)
            {
                SolutionM s = sender as SolutionM;
                switch (e.PropertyName)
                {
                    case "ConcentrationId":
                    case "Date_Begin":
                        dbOp.Solutions.Update(s);
                        break;
                }
            }
        }


    }
}
