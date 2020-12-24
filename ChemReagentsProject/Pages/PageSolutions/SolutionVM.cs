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
        IPageSolution page;
        public SolutionVM(IDbCrud cr, IReportServ report, IPageSolution pg)
        {
            dbOp = cr;
            rep = report;
            page = pg;
            InarService.ChangeClick += InarService_ChangeClick;
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
            get => solutLineList;
            set
            {
                solutLineList = value;
            }
        }




        public void Dispose()       //но он не вызывается ниоткуда
        {
            InarService.ChangeClick -= InarService_ChangeClick;
        }

    }
}
