using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.Pages.WinQuestion;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.Pages.PageSolutions
{
    class SolutionVM : IRecognizable, INotifyPropertyChanged, IDisposable
    {
        IDbCrud dbOp;
        IReportServ rep;
        public SolutionVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
            InarService.ChangeClick += InarService_ChangeClick;
            InarService.ReagentChange += InarService_ReagentChange;
            InarService.SupplyChange += InarService_SupplyChange;
            InarService.SolLineTextChange += InarService_SolLineTextChange;
            startDate = DateTime.Now.AddDays(-7);
            endDate = DateTime.Now.AddDays(1);
        }

        private void InarService_SolLineTextChange(object sender, string e)
        {
            if (selectLine != null)
            {
                SelectLine.NameOtherComp = e;
            }
        }

        private void InarService_SupplyChange(object sender, SupplyM e)
        {
            if (SelectLine != null)
            {
                SelectLine.SelectSuppl = e;
            }
        }

        private void InarService_ReagentChange(object sender, ReagentM e)
        {
            if (SelectLine != null)
            {
                SelectLine.SelectReag = e;
            }
        }

        private void InarService_ChangeClick(object sender, EventArgs e)
        {
            if (selectSolution != null)
            {
                WinEditRecInSuppl win = new WinEditRecInSuppl(dbOp, rep, selectSolution);
                if (win.ShowDialog() == true)
                {
                    var saveex=rep.AcceptRecipe(selectSolution.Id, DateTime.Now);
                    if (saveex != null) MessageBox.Show(saveex.Message);
                    OnPropertyChanged("SolutionList");

                }

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
                //solutionList = dbOp.Solutions.GetList();
                solutionList = dbOp.Solutions.GetListbyDate(StartDate, EndDate);
                foreach (SolutionM s in solutionList)
                {
                    s.PropertyChanged += Solution_PropertyChanged;
                }
                solutionList.CollectionChanged += SolutionList_CollectionChanged;


                return solutionList;
            }
        }

        private SolutionM selectSolution;
        public SolutionM SelectSolution
        {
            get => selectSolution;
            set
            {
                if (value != null && value.Id != 0)
                {
                    selectSolution = value;
                    OnPropertyChanged("SolutLineList");
                }
            }
        }


        private void SolutionList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SolutionM s = new SolutionM() { Date_Begin = DateTime.Now };
                    var ex = dbOp.Solutions.Create(s);
                    if (ex != null) MessageBox.Show(ex.Message);
                    OnPropertyChanged("SolutionList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    QuestWin win = new QuestWin("Вы точно хотите удалить этот раствор?");
                    if (win.ShowDialog() == true)
                    {
                        dbOp.Solutions.Delete((e.OldItems[0] as SolutionM).Id);
                    }
                    else
                    {
                        OnPropertyChanged("ConcentrList");
                    }
                    break;
            }
        }

        private void Solution_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            SolutionM s = sender as SolutionM;
            switch (e.PropertyName)
            {
                case "ConcentrationId":
                case "Date_Begin":
                case "RecipeName":
                case "ConcentrName":

                    var ex =dbOp.Solutions.Update(s);
                    if (ex != null) MessageBox.Show(ex.Message);
                    break;
            }

        }

        private ObservableCollection<SolutionLineM> solutLineList;
        public ObservableCollection<SolutionLineM> SolutLineList
        {
            get
            {
                if (selectSolution != null)
                {
                    solutLineList = dbOp.SolutLines.SolutionLineBySolut(selectSolution.Id);
                    var reags = dbOp.Reagents.GetList();
                    reags.Add(new ReagentM() { Id = -3, Name = "Свой", });   //и чё ты мне сделоеш
                    foreach (SolutionLineM sl in solutLineList)
                    {
                        sl.ReagList = reags;
                        if (sl.SupplyId != null)
                        {
                            int reagid = (dbOp.Supplies.GetItem((int)sl.SupplyId)).ReagentId;
                            foreach (ReagentM r in reags)
                            {
                                if (r.Id == reagid)
                                {
                                    sl.SelectReag = r;
                                    var supplies = rep.SupplyByReag(reagid);
                                    sl.SupplyList = supplies;
                                    foreach (SupplyM ss in supplies)
                                    {
                                        if (ss.Id == sl.SupplyId)
                                        {
                                            sl.SelectSuppl = ss;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }

                        sl.PropertyChanged += SolutLines_PropertyChanged;
                    }
                    solutLineList.CollectionChanged += SolutLineList_CollectionChanged;
                    return solutLineList;
                }
                else return new ObservableCollection<SolutionLineM>();


            }
            set
            {
                //OnPropertyChanged("SolutLineList");
                //solutLineList = value;
            }
        }

        private void SolutLines_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is SolutionLineM sl)
            {
                if (e.PropertyName == "SelectReag")
                {

                    if (sl.Id != -3)
                    {
                        sl.SupplyList = rep.SupplyByReag(sl.SelectReag.Id);
                        if (sl.SupplyList != null && sl.SupplyList.Count > 0)
                        {
                            sl.SelectSuppl = sl.SupplyList[0];
                        }
                        else
                        {
                            sl.SelectSuppl = null;
                            //sl.SupplyList = null;
                        }
                    }
                    else
                    {
                        //sl.SupplyList = null;
                        sl.SelectSuppl = null;
                    }
                }
                else
                {
                    switch (e.PropertyName)
                    {
                        case "SolutionId":
                        case "SupplyId":
                        case "NameOtherComp":
                            var ex =dbOp.SolutLines.Update(sl);
                            if (ex != null) MessageBox.Show(ex.Message);
                            break;
                        case "Count":   //нужно смотреть, верная ли цифра введена
                            if (sl.SupplyId == null)
                            {
                                var ex2 =dbOp.SolutLines.Update(sl);
                                if (ex2 != null) MessageBox.Show(ex2.Message);
                            }
                            else
                            {
                                float countwithoutline = dbOp.Supplies.GetItem((int)sl.SupplyId).Count - rep.GetSupplyStrings((int)sl.SupplyId, sl.Id).Summ;
                                if (countwithoutline - sl.Count < 0)
                                {
                                    if (new QuestWin("Введённого количества нет в выбранной поставке, ввести максимальное количество?").ShowDialog() == true)
                                    {
                                        if(countwithoutline>=0)
                                        {
                                            sl.Count = countwithoutline;

                                            var ex3=dbOp.SolutLines.Update(sl);
                                            if (ex3 != null) MessageBox.Show(ex3.Message);
                                            OnPropertyChanged("SolutLineList");
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    var ex4=dbOp.SolutLines.Update(sl);
                                    if (ex4 != null) MessageBox.Show(ex4.Message);
                                }
                            }
                            break;
                    }
                }
            }





        }

        private void SolutLineList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (SelectSolution != null)
                    {
                        SolutionLineM sl = new SolutionLineM() { SolutionId = SelectSolution.Id };
                        var ex =dbOp.SolutLines.Create(sl);
                        if (ex != null) MessageBox.Show(ex.Message);
                        OnPropertyChanged("SolutLineList");
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    dbOp.SolutLines.Delete((e.OldItems[0] as SolutionLineM).Id);
                    break;
            }
        }

        private SolutionLineM selectLine;
        public SolutionLineM SelectLine
        {
            get => selectLine;
            set
            {
                selectLine = value;
            }
        }



        private RelayCommand tabCommand;
        public RelayCommand TabCommand      //
        {
            get
            {
                return tabCommand ?? (tabCommand = new RelayCommand(obj =>
                {
                    //var a = rep.SupplyByReag(1);
                    //var b = rep.SupplyByReag(2);
                    //var c = rep.SupplyByReag(1);


                    //var a = rep.SupplyByReagOnlyActual(1, DateTime.Now);
                    //var b = rep.SupplyByReagOnlyActual(2, DateTime.Now);
                    //var c = rep.SupplyByReagOnlyActual(1, DateTime.Now);

                }));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
                OnPropertyChanged("SolutionList");
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
                OnPropertyChanged("SolutionList");
            }
        }


        public void Dispose()
        {
            InarService.ChangeClick -= InarService_ChangeClick;
            InarService.ReagentChange -= InarService_ReagentChange;
            InarService.SupplyChange -= InarService_SupplyChange;
            InarService.SolLineTextChange -= InarService_SolLineTextChange;
        }

    }
}
