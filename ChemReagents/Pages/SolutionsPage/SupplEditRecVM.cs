using BLL.Interfaces;
using BLL.Models;
using BLL.Services.FIlters;
using ChemReagents.Additional;
using ChemReagents.Pages.DialogWins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.SolutionsPage
{
    class SupplEditRecVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        SolutionM editSolut;
        SolutionM TempSolut;
        public SupplEditRecVM(IDBCrud cr, IReportServ report, SolutionM s)
        {
            dbOp = cr;
            rep = report;
            editSolut = s;
            TempSolut = dbOp.Solutions.GetItem(editSolut.Id);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public ObservableCollection<RecipeM> RecipeList
        {
            get
            {
                var ret = dbOp.Recipes.GetList();
                ret.Add(new RecipeM() { Id = -1, Name = "Свой рецепт(название ниже)" });
                return ret;
            }
        }
        public int SelectRecipeId
        {
            get { if (TempSolut.RecipeId != null) return (int)TempSolut.RecipeId; else return -1; }
            set
            {
                if (value > 0) TempSolut.RecipeId = value;
                else { TempSolut.RecipeId = null; }
                TempSolut.ConcentrationId = null;
                OnPropertyChanged("SelectRecipeId"); OnPropertyChanged("ConcentrList");
                OnPropertyChanged("SelectConcentrId"); OnPropertyChanged("RecipeName"); OnPropertyChanged("ConcentrName");
                OnPropertyChanged("SolutLineList");
            }
        }

        public ObservableCollection<ConcentrationM> ConcentrList
        {
            get
            {
                var ret = dbOp.Concentrations.GetList(new ConcentrationFilter() { RecipeId = SelectRecipeId });
                ret.Add(new ConcentrationM() { Id = -1, Name = "Своя концентрация" });

                OnPropertyChanged("SelectConcentrId");

                return ret;
            }

        }

        public int SelectConcentrId
        {
            get { if (TempSolut.ConcentrationId == null) return -1; else return (int)TempSolut.ConcentrationId; }
            set { if (value > 0) TempSolut.ConcentrationId = value; else TempSolut.ConcentrationId = null; OnPropertyChanged("ConcentrName"); OnPropertyChanged("SolutLineList"); }
        }

        public string RecipeName { get => TempSolut.RecipeName; set { TempSolut.RecipeName = value; } }
        public string ConcentrName { get => TempSolut.ConcentrName; set { TempSolut.ConcentrName = value; } }
        public uint Count { get => (uint)TempSolut.Count; set { TempSolut.Count = value; OnPropertyChanged("Count"); OnPropertyChanged("SolutLineList"); } }

        public ObservableCollection<SolutionLineM> SolutLineList
        {
            get
            {
                var ret = rep.GetSuppliesForRecipe(TempSolut.Id, SelectRecipeId, SelectConcentrId, TempSolut.Date_Begin, Count);
                return ret;
            }
        }

        private RelayCommand acceptCommand;
        public RelayCommand AcceptCommand
        {
            get => acceptCommand ?? (acceptCommand = new RelayCommand(obj =>
            {
                switch (obj as string)
                {
                    case "Accept":
                        bool b = false;
                        var Sll = SolutLineList;
                        foreach (var SL in Sll)
                        {
                            if (!SL.CanTakeRec) { b = true; break; }
                        }
                        if (b )
                        {
                            new MyDialogWin("Нельзя добавить такой рецепт", false).ShowDialog();
                            return;
                        }
                        var ex = dbOp.Solutions.Update(TempSolut);
                        if (ex != null) new ErrorWin(ex).ShowDialog();
                        rep.DeleteSolutLines(TempSolut.Id);
                        foreach (var SL in Sll)
                        {
                            SL.SolutionId = TempSolut.Id;
                            var ex2 = dbOp.SolutionLines.Create(SL);
                            if (ex2 != null) new ErrorWin(ex2).ShowDialog();
                        }
                        WindowService.CloseWindow(this, true);
                        break;
                    case "Cancel":
                        WindowService.CloseWindow(this, false);
                        break;
                }
            }));
        }

    }
}
