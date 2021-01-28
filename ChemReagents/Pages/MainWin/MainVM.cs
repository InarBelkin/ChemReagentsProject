﻿using BLL.Interfaces;
using ChemReagents.Additional;
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
            mainpage.SetPage(new ReagentsPage.PageReag(dbOp, rep));
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
                            mainpage.SetPage(new ReagentsPage.PageReag( dbOp, rep));
                            //CurrentPage = new ReagentsPage.PageReag();
                            break;
                        case "Suppliers":
                            mainpage.SetPage(new SuppliersPage.PageSuppliers());
                            //CurrentPage = new SuppliersPage.PageSuppliers();
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

    }
}