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
using ChemReagentsProject.Pages.WinQuestion;

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

        #region RecipeList
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
            var ex = dbOp.SolutRecipes.Update(s);
            if (ex != null) MessageBox.Show(ex.Message);
        }

        private void RecipeList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SolutionRezipeM newreag = new SolutionRezipeM();
                    var ex = dbOp.SolutRecipes.Create(newreag);
                    if (ex != null) MessageBox.Show(ex.Message);
                    OnPropertyChanged("RecipeList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var s = rep.GetRecipeLine((e.OldItems[0] as SolutionRezipeM).Id);
                    if (s.Count > 0)
                    {
                        WinQuestion.QuestWin win = new WinQuestion.QuestWin("Вы действительно хотите удалить рецепт?");
                        if (win.ShowDialog() == true)
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
                        //page.SetItemSource(ReagentList);
                        selectRecipe = s;
                        //OnPropertyChanged("RecipeLineList");
                        OnPropertyChanged("ConcentrList");

                    }

                }
            }
        }
        #endregion

        #region Concentration   
        private ObservableCollection<ConcentrationM> concentrList;
        public ObservableCollection<ConcentrationM> ConcentrList
        {
            get
            {
                if (selectRecipe != null)
                {
                    concentrList = rep.ConcentrbyRecipe(selectRecipe.Id);
                    foreach (ConcentrationM c in concentrList)
                    {
                        c.PropertyChanged += Concentr_PropertyChanged;
                    }
                    concentrList.CollectionChanged += ConcentrList_CollectionChanged;
                    return concentrList;
                }
                else return null;
            }
            set { OnPropertyChanged("ConcentrList"); }
        }

        private void ConcentrList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    ConcentrationM newconc = new ConcentrationM() { SolutionRecipeId = selectRecipe.Id };
                    var ex = dbOp.Concentrations.Create(newconc);
                    if (ex != null) MessageBox.Show(ex.Message);
                    OnPropertyChanged("ConcentrList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    QuestWin win = new QuestWin("Вы точно хотите удалить эту концетрацию?");
                    if (win.ShowDialog() == true)
                    {
                        dbOp.Concentrations.Delete((e.OldItems[0] as ConcentrationM).Id);
                    }
                    else
                    {
                        OnPropertyChanged("ConcentrList");
                    }
                    break;
            }
        }

        private void Concentr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ConcentrationM c)
            {
                var ex =dbOp.Concentrations.Update(c);
                if (ex != null) MessageBox.Show(ex.Message);
            }
        }

        private ConcentrationM selectConcentr;
        public ConcentrationM SelectConcentr
        {
            get
            {
                return selectConcentr;
            }
            set
            {
                if (value is ConcentrationM c)
                {
                    if (c.Id != 0)
                    {
                        page.SetItemSource(ReagentList);
                        selectConcentr = c;
                        OnPropertyChanged("RecipeLineList");
                    }
                }
            }
        }

        #endregion

        private ObservableCollection<SolutRezLineM> recipeLineList;
        public ObservableCollection<SolutRezLineM> RecipeLineList
        {
            get
            {
                if (selectConcentr != null)
                {

                    recipeLineList = rep.GetRecipeLine(selectConcentr.Id);
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
                var ex =dbOp.SolutRecLines.Update(L);
                if (ex != null) MessageBox.Show(ex.Message);
            }
        }

        private void RecipeLine_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var ReagentList = dbOp.Reagents.GetList();  //если никто не вводил реактивы, это сработает
                    if (ReagentList.Count > 0)
                    {
                        SolutRezLineM newline = new SolutRezLineM() { ReagentId = dbOp.Reagents.GetList()[0].Id, ConcentrationId = SelectConcentr.Id };
                        var ex = dbOp.SolutRecLines.Create(newline);
                        if (ex != null) MessageBox.Show(ex.Message);
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
