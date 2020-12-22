using BLL.Interfaces;
using ChemReagentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.Specialized;

namespace ChemReagentsProject.Pages.PageReziepe
{
    class ReziepeVM : IRecognizable, INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        public ReziepeVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
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

        private ObservableCollection<SolutionRezipeM> recipeList;
        public ObservableCollection<SolutionRezipeM> RecipeList
        {
            get
            {
                recipeList = dbOp.SolutRecipes.GetList();
                foreach (SolutionRezipeM s in recipeList)
                {
                    s.PropertyChanged += RecipePropCh;
                }
                recipeList.CollectionChanged += RecipeList_CollectionChanged;
                return recipeList;

            }
            set { }
        }

        private void RecipePropCh(object sender, PropertyChangedEventArgs e)
        {
            SolutionRezipeM s = sender as SolutionRezipeM;
            dbOp.SolutRecipes.Update(s);
        }

        private void RecipeList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SolutionRezipeM newreag = new SolutionRezipeM();
                    dbOp.SolutRecipes.Create(newreag);
                    OnPropertyChanged("RecipeList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    dbOp.SolutRecipes.Delete((e.OldItems[0] as SolutionRezipeM).Id);
                    break;
            }
        }

        private SolutionRezipeM selectRecipe;
        public object SelectRecipe
        {
            get
            {
                return null;
            }
            set
            {
                if (value is SolutionRezipeM s)
                {
                    if (s.Id != 0)
                    {
                        selectRecipe = s;
                        OnPropertyChanged("RecipeLineList");
                    }

                }
            }
        }

        //private SolutRezLineM recipeLineList;
        public ObservableCollection<SolutRezLineM> RecipeLineList
        {
            get
            {
                if (selectRecipe != null)
                {
                    ObservableCollection<SolutRezLineM> recipeLineList = rep.GetRecipeLine(selectRecipe.Id);
                    recipeLineList.CollectionChanged += RecipeLine_CollectionChanged;
                    return recipeLineList;
                }
                else return null;

            }
            set
            {
                OnPropertyChanged("RecipeLineList");
            }
        }

        private void RecipeLine_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SolutRezLineM newline = new SolutRezLineM();
                    dbOp.SolutRecLines.Create(newline);
                    OnPropertyChanged("RecipeLineList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    dbOp.SolutRecLines.Delete((e.OldItems[0] as SolutRezLineM).Id);
                    break;
            }
        }

        public ObservableCollection<ReagentM> ReagentList
        {
            get
            {
                return dbOp.Reagents.GetList();
            }
        }
    }
}
