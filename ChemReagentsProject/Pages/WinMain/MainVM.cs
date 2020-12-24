using BLL.Interfaces;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.Pages.PageReagents;
using ChemReagentsProject.Pages.PageReziepe;
using ChemReagentsProject.Pages.PageSolutions;
using System;
using System.ComponentModel;
using System.Globalization;


namespace ChemReagentsProject.ViewModel //Типа изменил
{
    partial class MainVM : INotifyPropertyChanged, IRecognizable
    {
        IDbCrud dbOp;
        IReportServ rep;
        Guid ThisGuid;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid GetGuid() => ThisGuid;

        public MainVM(IDbCrud cr, IReportServ repserv)
        {
            //MainWin = w;
            dbOp = cr;
            rep = repserv;
            ThisGuid = Guid.NewGuid();

            NavService.Navigation.LoadDone += Navigation_LoadDone;
        }

        private void Navigation_LoadDone(object sender, IRecognizable e)
        {
            if (e.GetGuid() == ThisGuid)
            {
                NavService.Navigation.Navigate(ThisGuid, new PageReag(dbOp, rep));
            }

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
                            NavService.Navigation.Navigate(ThisGuid, new PageReag(dbOp, rep));
                            //MainWin.ChangePage(PReag);
                            break;
                        case "Reziepe":
                            NavService.Navigation.Navigate(ThisGuid, new PageReziepe(dbOp, rep));
                            //MainWin.ChangePage(PSolutRec);
                            break;
                        case "Solution":
                            NavService.Navigation.Navigate(ThisGuid, new PageSolution(dbOp, rep));
                            break;

                        default:
                            break;
                    }
                }));
            }
        }




    }
}
