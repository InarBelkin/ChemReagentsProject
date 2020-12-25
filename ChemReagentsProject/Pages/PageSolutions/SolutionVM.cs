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
                solutionList = dbOp.Solutions.GetList();
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
                    dbOp.Solutions.Create(s);
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
                    dbOp.Solutions.Update(s);
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
                        sl.PropertyChanged += SolutLines_PropertyChanged;
                        // sl.ReagList = 
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
            if (e.PropertyName == "SelectReag")
            {
                if (sender is SolutionLineM sl)
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

            }
        }

        private void SolutLineList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

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

        public void Dispose()       //но он не вызывается ниоткуда, а должно вообще то
        {
            InarService.ChangeClick -= InarService_ChangeClick;
            InarService.ReagentChange -= InarService_ReagentChange;
        }

    }
}
