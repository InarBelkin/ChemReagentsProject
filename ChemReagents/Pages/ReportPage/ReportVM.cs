using BLL.Interfaces;
using BLL.Models.OtherModels;
using ChemReagents.Additional;
using ChemReagents.Pages.DialogWins;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChemReagents.Pages.ReportPage
{
    class ReportVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        public ReportVM(IDBCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
            SelYear = (uint)DateTime.Today.Year;
            SelectMonth = (byte)DateTime.Today.Month;
            IsCreate = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var s in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
            }
        }

        bool IsCreate;
        bool canExp;
        private string info;
        public string Info
        {
            get
            {
                if (RepM == null) return "Этот отчёт ещё не создавался";
                else return "Этот отчёт был создан " + RepM.RealDate.ToString("dd.MM.yyyy");
            }
            set { info = value; OnPropertyChanged("Info"); }
        }


        private uint selYear;
        public uint SelYear { get => selYear; set { if (value > 2000 && value < 2100) selYear = value; OnPropertyChanged("SelYear", "RepM", "Info"); IsCreate = false;  } }

        public MonthM[] MonthList => rep.GetMonths();
        private byte selectMonth;
        public byte SelectMonth { get => selectMonth; set { selectMonth = value; OnPropertyChanged("SelectMonth", "RepM", "Info"); IsCreate = false;  } }

        //  private ReportM repM;
        public ReportM RepM
        {
            get
            { return rep.GetMonthRep(SelYear, SelectMonth); }
        }

        private RelayCommand create;
        public RelayCommand Create
        {
            get => create ?? (create = new RelayCommand(obj =>
            {
                (List<MonthReportLineM>, List<MonthReportLineM>) a = rep.GetListReport(SelYear, SelectMonth);
                OstatkList = a.Item1;
                WriteList = a.Item2;
                IsCreate = true;
                //if (rep.GetMonthRep(SelYear, SelectMonth) == null)   //если не нашли рецепт, то можно его создать
                //{

                //}
            }));
        }

        private List<MonthReportLineM> ostatkList;
        public List<MonthReportLineM> OstatkList
        { get => ostatkList; set { ostatkList = value; OnPropertyChanged("OstatkList"); } }

        private List<MonthReportLineM> writeList;
        public List<MonthReportLineM> WriteList
        {
            get => writeList; set { writeList = value; OnPropertyChanged("WriteList"); }
        }

        private RelayCommand accept;
        public RelayCommand Accept
        {
            get => accept ?? (accept = new RelayCommand(obj =>
            {
                if (!IsCreate) new MyDialogWin("Сначала создайте отчёт по этой дате", false).ShowDialog();
                else if (WriteList == null || WriteList.Count == 0) new MyDialogWin("Не создан отчёт или нечего применять", false).ShowDialog();
                else
                {
                    if (RepM == null) rep.CreateMonthRep(new ReportM() { RealDate = DateTime.Today, TimeRep = new DateTime((int)SelYear, SelectMonth, 1), });
                    foreach (var m in WriteList)
                    {
                        rep.AcceptWriteOff(m, RepM);
                    }
                }

            }));
        }

        private RelayCommand export;
        public RelayCommand Export
        {
            get => export ?? (export = new RelayCommand(obj =>
            {




            }));
        }

    }
}
