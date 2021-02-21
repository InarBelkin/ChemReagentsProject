using BLL.Interfaces;
using BLL.Models;
using ChemReagents.Additional;
using ChemReagents.Pages.DialogWins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.SupplyWin
{
    class EditConsumptionVM : INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        SupplyStingM consump;
        public EditConsumptionVM(IDBCrud db, IReportServ r, SupplyStingM sstring)
        {
            dbOp = db; rep = r;
            consump = sstring;
            NameB = consump.Name;
            DateB = consump.DateBegin;
            CountB = consump.Count;

        }

        private string nameB;
        public string NameB
        {
            get => nameB;
            set
            { nameB = value; OnPropertyChanged("NameB"); }
        }

        private DateTime dateB;
        public DateTime DateB
        {
            get => dateB;
            set
            { dateB = value; OnPropertyChanged("DateB"); }
        }

        private decimal countB;
        public decimal CountB
        {
            get => countB;
            set
            { countB = value; OnPropertyChanged("CountB"); }
        }

        private RelayCommand comButton;
        public RelayCommand ComButton      //
        {
            get
            {
                return comButton ?? (comButton = new RelayCommand(obj =>
                {
                    switch (obj as string)
                    {
                        case "Save":
                            if (NameB != null && NameB.Length > 0 && CountB > 0)
                            {
                                consump.Name = NameB;
                                consump.DateBegin = DateB;
                                consump.Count = CountB;
                                WindowService.CloseWindow(this, true);
                            }
                            else
                            {
                                new MyDialogWin("Что-то не довведено", false).ShowDialog();
                            }
                            break;
                        case "Cancel":
                            WindowService.CloseWindow(this, false);
                            break;
                        default:
                            break;
                    }



                }));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
