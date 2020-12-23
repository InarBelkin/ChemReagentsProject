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
using System.Windows;

namespace ChemReagentsProject.Pages.PageReziepe
{
    class ReziepeVM : IRecognizable, INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        IPageRezipe page;
        public ReziepeVM(IDbCrud cr, IReportServ report, IPageRezipe pg)
        {
            dbOp = cr;
            rep = report;
            page = pg;

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
                    var s = rep.GetRecipeLine((e.OldItems[0] as SolutionRezipeM).Id);
                    if(s.Count>0)
                    {
                        WinQuestion.QuestWin win = new WinQuestion.QuestWin("Вы действительно хотите удалить рецепт?");
                        if(win.ShowDialog() == true)
                        {
                            dbOp.SolutRecipes.Delete((e.OldItems[0] as SolutionRezipeM).Id);
                        }
                        else
                        {
                            OnPropertyChanged("RecipeList");
                        }
                    }
                    else
                    {
                        dbOp.SolutRecipes.Delete((e.OldItems[0] as SolutionRezipeM).Id);
                    }
                    break;
            }
        }

        private SolutionRezipeM selectRecipe;
        public object SelectRecipe
        {
            get
            {
                return selectRecipe;
            }
            set
            {
                if (value is SolutionRezipeM s)
                {
                    if (s.Id != 0)
                    {
                        //var a = dbOp.Reagents.GetList();
                        page.SetItemSource(ReagentList);
                        selectRecipe = s;
                        OnPropertyChanged("RecipeLineList");

                    }

                }
            }
        }

        private ObservableCollection<SolutRezLineM> recipeLineList;
        public ObservableCollection<SolutRezLineM> RecipeLineList
        {
            get
            {
                if (selectRecipe != null)
                {

                    recipeLineList = rep.GetRecipeLine(selectRecipe.Id);
                    foreach (SolutRezLineM s in recipeLineList)
                    {
                        s.PropertyChanged += RecLine_PropertyChanged;
                    }
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

        private void RecLine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is SolutRezLineM L)
            {
                dbOp.SolutRecLines.Update(L);
            }
        }

        private void RecipeLine_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var ReagentList = dbOp.Reagents.GetList();
                    if(ReagentList.Count>0)
                    {
                        SolutRezLineM newline = new SolutRezLineM() { ReagentId = dbOp.Reagents.GetList()[0].Id, SolutionRecipeId = selectRecipe.Id };
                        dbOp.SolutRecLines.Create(newline);
                        OnPropertyChanged("RecipeLineList");
                    }
                    else
                    {
                        MessageBox.Show("Добавьте хотя бы один реактив");
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    dbOp.SolutRecLines.Delete((e.OldItems[0] as SolutRezLineM).Id);
                    break;
            }
        }

        public ObservableCollection<ReagentM> ReagentList => dbOp.Reagents.GetList();
    }
}
