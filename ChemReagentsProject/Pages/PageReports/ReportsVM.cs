using BLL.Interfaces;
using BLL.Models.OtherModels;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
//using Word = Microsoft.Office.Interop.Word;

namespace ChemReagentsProject.Pages.PageReports
{
    class ReportsVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        bool isSaved;
        public ReportsVM(IDbCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
            isSaved = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand generateCommand;
        public RelayCommand GenerateCommand
        {
            get
            {
                return generateCommand ?? (generateCommand = new RelayCommand(obj =>
                {
                    isSaved = false;
                    GenerateReport();
                   
                }));
            }
        }

        private void GenerateReport()
        {
            ReportList = rep.GetMonthReport(RepBeg, RepEnd);
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(obj =>
                {
                    if (ReportList == null) MessageBox.Show("Сначала сгенерируйте отчёт");
                    else
                    {
                        DisplayInExcel(ReportList);


                    }


                }));
            }
        }

        void DisplayInExcel(List<MonthReportM> accounts)
        {
            var excelApp = new Excel.Application();
            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collection returned
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template.
            // Because no argument is sent in this example, Add creates a new workbook.
            excelApp.Workbooks.Add();

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheet.Cells[1, "A"] = "Название реагента";
            workSheet.Cells[1, "B"] = "Количество";
            workSheet.Cells[1, "C"] = "Списание или расход";

            int row = 1;
            foreach(var str in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = str.ReagentName;
                workSheet.Cells[row, "B"] = str.Count;
                workSheet.Cells[row, "C"] = str.status;
            }
            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();

            isSaved = true;

        }


        private RelayCommand writeOff;
        public RelayCommand WriteOff
        {
            get
            {
                return writeOff ?? (writeOff = new RelayCommand(obj =>
                {
                    if(ReportList == null)
                    {
                        MessageBox.Show("Сначала сгенерируйте отчёт");
                    }
                    else if(!isSaved)
                    {
                        MessageBox.Show("Сначала сохраните отчёт");
                    }
                    else
                    {
                        foreach( var s in ReportList)
                        {
                            dbOp.Supplies.GetItem(s.SupplyId);

                        }
                    }



                  


                }));
            }
        }




        private DateTime repBeg;
        public DateTime RepBeg
        {
            get => repBeg;
            set
            {
                repBeg = value; OnPropertyChanged("RepBeg");
            }
        }

        private DateTime repEnd;
        public DateTime RepEnd
        {
            get => repEnd;
            set
            {
                repEnd = value; OnPropertyChanged("RepEnd");
            }
        }

        private List<MonthReportM> reportList;
        public List<MonthReportM> ReportList
        {
            get => reportList;
            set
            {
                reportList = value;
                OnPropertyChanged("ReportList");
            }
        }



    }
}
