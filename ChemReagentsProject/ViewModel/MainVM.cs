﻿using BLL.Interfaces;
using ChemReagentsProject.Interfaces;
using System.ComponentModel;
using System.Globalization;

namespace ChemReagentsProject.ViewModel //Типа изменил
{
    partial class MainVM : INotifyPropertyChanged
    {
        IDbCrud dbOp;
        IReportServ rep;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainVM(IMainWin w, IDbCrud cr,IReportServ repserv)
        {
            MainWin = w;
            dbOp = cr;
            rep = repserv;
            MainWin.ChangePage(PReag);

        }

        private RelayCommand tabCommand;
        public RelayCommand TabCommand      //
        {
            get
            {
                return tabCommand ?? (tabCommand = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Reagent":
                            MainWin.ChangePage(PReag);
                            break;
                        case "Reziepe":
                            MainWin.ChangePage(PSolutRec);
                            break;
                        default:
                            break;
                    }
                }));
            }
        }

       


    }
}
