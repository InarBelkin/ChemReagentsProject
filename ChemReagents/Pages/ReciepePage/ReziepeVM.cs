using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ChemReagents.Pages.DialogWins;
using BLL.Services.FIlters;
using System.Windows;
using ChemReagents.AdditionalWins.SettingsWin;

namespace ChemReagents.Pages.ReciepePage
{
    class ReziepeVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        IPageRecipe page;
        public ReziepeVM(IDBCrud cr, IReportServ report, IPageRecipe pagerec)
        {
            dbOp = cr;
            rep = report;
            page = pagerec;
            page.SetReagents(dbOp.Reagents.GetList());
            page.ChangeReagent += Page_ChangeReagent;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Visibility DevModeVis
        {
            get
            {
                if (Settings.DevMode) return Visibility.Visible;
                else return Visibility.Hidden;
            }
        }

        public ObservableCollection<RecipeM> RecipeList
        {
            get
            {
                ObservableCollection<RecipeM> ret = dbOp.Recipes.GetList();
                foreach (RecipeM r in ret)
                {
                    r.PropertyChanged += Recipe_PropertyChanged;
                }
                ret.CollectionChanged += Recipe_CollectionChanged;
                return ret;
            }
        }

        private void Recipe_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RecipeM newrec = e.NewItems[0] as RecipeM;
                    var ex = dbOp.Recipes.Create(newrec);
                    if (ex != null) new ErrorWin(ex).ShowDialog();
                    else
                    {
                        newrec.PropertyChanged += Recipe_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RecipeM delrec = e.OldItems[0] as RecipeM;
                    if (new MyDialogWin($"Вы точно хотите удалить этот рецепт ({delrec.Name})?", true).ShowDialog() == true)
                    {
                        dbOp.Recipes.Delete(delrec.Id);
                    }
                    else
                    {
                        OnPropertyChanged("RecipeList");
                    }
                    break;
            }
        }

        private void Recipe_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var ex = dbOp.Recipes.Update(sender as RecipeM);
            if (ex != null) new ErrorWin(ex).ShowDialog();
        }

        private RecipeM selectRecipe;
        public RecipeM SelectRecipe
        {
            get => selectRecipe;
            set
            {
                selectRecipe = value;
                OnPropertyChanged("SelectRecipe");
                OnPropertyChanged("ConcentrList");
            }
        }

        public ObservableCollection<ConcentrationM> ConcentrList
        {
            get
            {
                if (SelectRecipe != null)
                {
                    var ret = dbOp.Concentrations.GetList(new ConcentrationFilter() { RecipeId = SelectRecipe.Id });
                    foreach (ConcentrationM c in ret)
                    {
                        c.PropertyChanged += Concentr_PropertyChanged;
                    }
                    ret.CollectionChanged += Concentr_CollectionChanged;
                    return ret;
                }
                return null;
            }
        }

        private void Concentr_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (SelectRecipe != null)
                    {
                        ConcentrationM fromconc=null;   //сохраняем сюда другую концентрацию
                        if(ConcentrList.Count >0)
                        {
                            fromconc = ConcentrList[0];
                        }
                        ConcentrationM newconc = e.NewItems[0] as ConcentrationM;   //создаём новую
                        newconc.SolutionRecipeId = SelectRecipe.Id;
                        var ex = dbOp.Concentrations.Create(newconc);
                        if (ex != null) new ErrorWin(ex).ShowDialog();
                        else
                        {
                            newconc.PropertyChanged += Concentr_PropertyChanged;
                            if (fromconc != null)    //Копируем из другой концентрации строки
                            {
                                var linesfrom = dbOp.Recipe_Lines.GetList(new RecipeLineFilter() { ConcentrationId = fromconc.Id });
                                foreach (var line in linesfrom)
                                {
                                    line.ConcentracionId = newconc.Id;
                                    line.Count = 0;
                                    dbOp.Recipe_Lines.Create(line);
                                }
                            }
                        }
                       
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ConcentrationM delconc = e.OldItems[0] as ConcentrationM;
                    if (new MyDialogWin($"Вы точно хотите удалить эту концентрацию ({delconc.Name})?", true).ShowDialog() == true)
                    {
                        dbOp.Concentrations.Delete(delconc.Id);
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
            var ex = dbOp.Concentrations.Update(sender as ConcentrationM);
            if (ex != null) new ErrorWin(ex).ShowDialog();
        }

        private ConcentrationM selectConcentr;
        public ConcentrationM SelectConcentr
        {
            get => selectConcentr;
            set
            {
                selectConcentr = value;
                OnPropertyChanged("SelectConcentr");
                OnPropertyChanged("RecipeLineList");
            }
        }

        public ObservableCollection<RecipeLineM> RecipeLineList
        {
            get
            {
                if (SelectConcentr != null)
                {
                    var ret = dbOp.Recipe_Lines.GetList(new RecipeLineFilter() { ConcentrationId = SelectConcentr.Id });
                    foreach (RecipeLineM r in ret)
                    {
                        r.PropertyChanged += RecipeLine_PropertyChanged;
                    }
                    ret.CollectionChanged += RecipeLine_CollectionChanged;
                    return ret;
                }
                else return null;
            }
        }

        private void RecipeLine_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (SelectConcentr != null)
                    {
                        var reagents = dbOp.Reagents.GetList();
                        if (reagents.Count > 0)
                        {
                            RecipeLineM newrecline = e.NewItems[0] as RecipeLineM;
                            newrecline.ConcentracionId = SelectConcentr.Id;
                            newrecline.ReagentId = reagents[0].Id;
                            var ex = dbOp.Recipe_Lines.Create(newrecline);
                            if (ex != null) new ErrorWin(ex).ShowDialog();
                            else
                            {
                                newrecline.PropertyChanged += RecipeLine_PropertyChanged;
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RecipeLineM delrec = e.OldItems[0] as RecipeLineM;
                    dbOp.Recipe_Lines.Delete(delrec.Id);
                    break;
            }
        }

        private void RecipeLine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var ex = dbOp.Recipe_Lines.Update(sender as RecipeLineM);
            if (ex != null) new ErrorWin(ex).ShowDialog();
        }

        private RecipeLineM selectRezLine;
        public RecipeLineM SelectRezLine
        {
            get => selectRezLine;
            set
            {
                selectRezLine = value;
            }
        }

        private void Page_ChangeReagent(object sender, ReagentM e)
        {
            if (e != null && SelectRezLine!=null)
            {
                SelectRezLine.SetReagent(e);
            }
        }
        public ObservableCollection<ReagentM> ReagentList
        {
            get => dbOp.Reagents.GetList();
        }
    }
}
