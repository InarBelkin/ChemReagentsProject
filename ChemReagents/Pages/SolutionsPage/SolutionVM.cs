using BLL.Interfaces;
using BLL.Models;
using BLL.Services.FIlters;
using ChemReagents.Additional;
using ChemReagents.Pages.DialogWins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.SolutionsPage
{
    class SolutionVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        IPageSolution page;
        public SolutionVM(IDBCrud cr, IReportServ report, IPageSolution pagesol)
        {
            dbOp = cr;
            rep = report;
            page = pagesol;
            page.ChangeRecipeButton += ChangeRecipeButton;
            page.SelectReagentChanged += SelectReagentChanged;
            page.SelectSupplyChanged += Page_SelectSupplyChanged;
            page.NameOtherCompChanged += NameOtherCompChanged;
            StartDate = DateTime.Today.AddDays(-7);
            EndDate = DateTime.Today.AddDays(1);
            page.DateBeginChanged += Page_DateBeginChanged;
            page.DateEndCHanged += Page_DateEndCHanged;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      

        private void Page_DateBeginChanged(object sender, DateTime e)
        {
            if(SelectSolution!=null)
            {
                SelectSolution.Date_Begin = e;
            }
        }

        private void Page_DateEndCHanged(object sender, DateTime e)
        {
            if (SelectSolution != null)
            {
                SelectSolution.Date_End = e;
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate; set { startDate = value; OnPropertyChanged("SolutionList"); OnPropertyChanged("StartDate"); }
        }
        private DateTime endDate;
        public DateTime EndDate
        { get => endDate; set { endDate = value; OnPropertyChanged("EndDate"); OnPropertyChanged("SolutionList"); } }

        public ObservableCollection<SolutionM> SolutionList
        {
            get
            {
                var ret = dbOp.Solutions.GetList(new SolutionFilter() { DateStart = StartDate, DateEnd = EndDate });
                foreach (var s in ret)
                {
                    s.PropertyChanged += Solut_PropertyChanged;
                }
                ret.CollectionChanged += Solut_CollectionChanged;
                return ret;
            }
        }

        private void Solut_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SolutionM newsolut = e.NewItems[0] as SolutionM;
                    newsolut.Date_Begin = DateTime.Today;
                    newsolut.Date_End = DateTime.Today.AddDays(1);
                    var ex = dbOp.Solutions.Create(newsolut);
                    if (ex != null) new ErrorWin(ex).ShowDialog();
                    else newsolut.PropertyChanged += Solut_PropertyChanged;
                    OnPropertyChanged("SolutionList");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    SolutionM delsolut = e.OldItems[0] as SolutionM;
                    if (new MyDialogWin("Вы точно хотите удалить этот раствор?", true).ShowDialog() == true)
                    {
                        dbOp.SolutionLines.Delete(delsolut.Id);
                    }
                    else OnPropertyChanged("SolutionList");
                    break;
            }
        }

        private void Solut_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var ex = dbOp.Solutions.Update(sender as SolutionM);
            if (ex != null) new ErrorWin(ex).ShowDialog();
        }

        private SolutionM selectSolution;
        public SolutionM SelectSolution
        {
            get => selectSolution;
            set
            {
                selectSolution = value;
                OnPropertyChanged("SelectSolution");
                OnPropertyChanged("SolutLineList");
            }
        }

        private void ChangeRecipeButton(object sender, EventArgs e)
        {
            if (SelectSolution != null)
            {
                if (new WinSupplEditRec(dbOp, rep, SelectSolution).ShowDialog() == true)
                {
                    OnPropertyChanged("SolutionList");
                }

            }
        }

        public ObservableCollection<SolutionLineM> SolutLineList
        {
            get
            {
                if (SelectSolution != null)
                {
                    var ret = dbOp.SolutionLines.GetList(new SolutionLineFilter() { SolutionId = SelectSolution.Id });
                    foreach (var sl in ret)
                    {
                        sl.PropertyChanged += SolutLine_PropertyChanged;
                        SetSupplList(sl, SelectSolution.Date_Begin);
                        if (sl.SupplyId != null) sl.CountBalance = rep.GetRemains((int)sl.SupplyId,new DateTime(), false, sl.SolutionId);
                    }
                    ret.CollectionChanged += SolutLines_CollectionChanged;
                    return ret;
                }
                else return null;
            }
        }

        private SolutionLineM selectSolutLine;
        public SolutionLineM SelectSolutLine
        {
            get => selectSolutLine;
            set { selectSolutLine = value; OnPropertyChanged("SelectRezLine"); }
        }

        public ObservableCollection<ReagentM> ReagentList
        {
            get
            {
                var ret = dbOp.Reagents.GetList();
                ret.Add(new ReagentM() { Id = -1, Name = "Свой реактив" });
                return ret;
            }
        }

        private void SelectReagentChanged(object sender, ReagentM e)
        {
            if (e != null && SelectSolutLine != null)
            {
                SelectSolutLine.ReagentId = e.Id;
                SetSupplList(SelectSolutLine, SelectSolution.Date_Begin);
                if (SelectSolutLine.SupplyList.Count > 0)
                {
                    SelectSolutLine.SupplyId = SelectSolutLine.SupplyList[0].Id;
                    SelectSolutLine.CountBalance = rep.GetRemains((int)SelectSolutLine.SupplyId,new DateTime(), false, SelectSolutLine.SolutionId);
                }
                else
                {
                    SelectSolutLine.CountBalance = 0;
                }
                if (SelectSolutLine.ReagentId != -1)
                {
                    if (e.IsWater) SelectSolutLine.Units = "мл.";
                    else SelectSolutLine.Units = "гр.";
                }
                else
                { SelectSolutLine.Units = ""; SelectSolutLine.SupplyId = null; }
            }
        }

        private void Page_SelectSupplyChanged(object sender, SupplyM e)
        {
            if (e != null && SelectSolutLine != null)
            {
                SelectSolutLine.SupplyId = e.Id;
                SelectSolutLine.CountBalance = rep.GetRemains((int)SelectSolutLine.SupplyId, new DateTime(), false, SelectSolutLine.SolutionId);
            }
        }

        private void NameOtherCompChanged(object sender, string e)
        {
            if (e != null && SelectSolutLine != null)
            {
                SelectSolutLine.NameOtherComponent = e;
            }
        }

        private void SetSupplList(SolutionLineM SL, DateTime DateToday)
        {
            if (SL.ReagentId > 0)
            {
                SL.SupplyList = dbOp.Supplies.GetList(new SupplyforSolutFilter() { ReagentId = SL.ReagentId, DateNow = DateToday });

                //if(SL.SupplyList.Count>0)
                //{
                //    SL.SupplyId = SL.SupplyList[0].Id;
                //}
            }
            else SL.SupplyList = new ObservableCollection<SupplyM>();
        }

        private void SolutLines_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (SelectSolution != null)
                    {
                        SolutionLineM newsolutline = e.NewItems[0] as SolutionLineM;
                        newsolutline.SolutionId = SelectSolution.Id;
                        newsolutline.ReagentId = -1;
                        SetSupplList(newsolutline, SelectSolution.Date_Begin);
                        var ex = dbOp.SolutionLines.Create(newsolutline);
                        if (ex != null) new ErrorWin(ex).ShowDialog();
                        else newsolutline.PropertyChanged += SolutLine_PropertyChanged;

                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    SolutionLineM oldsol = e.OldItems[0] as SolutionLineM;
                    dbOp.SolutionLines.Delete(oldsol.Id);
                    break;


            }
        }

        private void SolutLine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var ex = dbOp.SolutionLines.Update(sender as SolutionLineM);
            if (ex != null) new ErrorWin(ex).ShowDialog();
        }






    }
}
