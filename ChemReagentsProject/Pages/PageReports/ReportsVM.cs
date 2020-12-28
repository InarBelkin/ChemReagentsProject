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
using OfficeOpenXml;
using System.IO;
using Microsoft.Win32;
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
            RepBeg = DateTime.Now.AddMonths(-1);
            RepEnd = DateTime.Now;
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
                        switch (obj as string)
                        {
                            case "Show":
                                DisplayInExcel(ReportList);
                                break;
                            case "Save":
                                DisplayInExel2(ReportList);
                                break;
                        }




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
            foreach (var str in accounts)
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

        private void DisplayInExel2(List<MonthReportM> accounts)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage p = new ExcelPackage())
            {

                var ws = p.Workbook.Worksheets.Add("Лист1");

                
                ws.Cells["A1:D1"].Merge = true;
                ws.Cells["A1"].Style.Font.Size = 16;
                ws.Cells["A1"].Value = "Акт списания за период с " + RepBeg.ToString("d") + " по " + RepEnd.ToString("d");
                ws.Cells["A2"].Value = "Название реагента";
                ws.Cells["B2"].Value = "Количество";
                ws.Cells["C2"].Value = "Списание или расход";

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(3).Width = 30;
                int row = 2;
                foreach (var str in accounts)
                {
                    row++;
                    ws.Cells[row, 1].Value = str.ReagentName;
                    ws.Cells[row, 2].Value = str.Count;
                    ws.Cells[row, 3].Value = str.status;

                }


                SaveFileDialog d = new SaveFileDialog();
                d.Title = "Сохранить отчёт";
                d.Filter = "Документ Excel (*.xlsx)|*.xlsx";

                if (d.ShowDialog() == true)
                {
                    string filename = d.FileName;
                    try
                    {
                        p.SaveAs(new FileInfo(filename));
                        isSaved = true;
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить отчёт", "Ошибка");
                    }

                }
            }
         


        }




        private RelayCommand writeOff;
        public RelayCommand WriteOff
        {
            get
            {
                return writeOff ?? (writeOff = new RelayCommand(obj =>
                {
                    if (ReportList == null)
                    {
                        MessageBox.Show("Сначала сгенерируйте отчёт");
                    }
                    else if (!isSaved)
                    {
                        MessageBox.Show("Сначала сохраните отчёт");
                    }
                    else
                    {
                        foreach (var s in ReportList)
                        {
                            if (s.isWrittenOff)
                            {
                                var sup = dbOp.Supplies.GetItem(s.SupplyId);
                                sup.State = BLL.Models.SupplStates.WriteOff;
                                dbOp.Supplies.Update(sup);
                            }


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
