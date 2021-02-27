using BLL.Interfaces;
using ChemReagents.Additional;
using ChemReagents.AdditionalWins.SettingsWin;
using ChemReagents.Pages.ReciepePage;
using ChemReagents.Pages.ReportPage;
using ChemReagents.Pages.SolutionsPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.MainWin
{
    class MainVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        IMainWin mainpage;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }

        public MainVM(IDBCrud cr, IReportServ repserv, IMainWin page)
        {
            dbOp = cr;
            rep = repserv;
            mainpage = page;
            Settings.Load();
            mainpage.SetPage(new ReagentsPage.PageReag(dbOp, rep));
            rep.LoadAll();
        }


        private RelayCommand tabCommand;
        public RelayCommand TabCommand
        {
            get
            {
                return tabCommand ?? (tabCommand = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Reagent":
                            mainpage.SetPage(new ReagentsPage.PageReag(dbOp, rep));
                            //CurrentPage = new ReagentsPage.PageReag();
                            break;
                        case "Reziepe":
                            mainpage.SetPage(new PageReziepe(dbOp, rep));
                            break;
                        case "Suppliers":
                            mainpage.SetPage(new SuppliersPage.PageSuppliers());
                            //CurrentPage = new SuppliersPage.PageSuppliers();
                            break;
                        case "Solution":
                            mainpage.SetPage(new PageSolutions(dbOp, rep));
                            break;
                        case "Report":
                            mainpage.SetPage(new PageReports(dbOp, rep));
                            break;
                        default:
                            break;
                    }
                }));
            }
        }

        object currentPage;
        object CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new RelayCommand(obj =>
                {
                    SettingsWin win = new SettingsWin();
                    win.ShowDialog();
                }));
            }
        }
    }
}
