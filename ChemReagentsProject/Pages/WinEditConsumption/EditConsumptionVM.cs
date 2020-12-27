using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.Pages.WinEditConsumption
{
    class EditConsumptionVM : INotifyPropertyChanged, IRecognizable
    {
        IDbCrud dbOp;
        IReportServ rep;
        ConsumptionM tempcons, EditCons;
        Guid ThisGuid;
        public EditConsumptionVM(IDbCrud cr, IReportServ report, ConsumptionM icons)
        {
            NameB = icons.Name;
            DateB = icons.DateBegin;
            CountB = icons.Count;
            ThisGuid = Guid.NewGuid();
            EditCons = icons;
            dateB = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid GetGuid()
        {
            return ThisGuid;
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

        private float countB;
        public float CountB
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
                                EditCons.Name = NameB;
                                EditCons.DateBegin = DateB;
                                EditCons.Count = CountB;
                                WindowService.CloseWindow(ThisGuid, true);
                            }
                            else
                            {
                                MessageBox.Show("Что-то не довведено");
                            }
                            break;
                        case "Cancel":
                            WindowService.CloseWindow(ThisGuid, false);
                            break;
                        default:
                            break;
                    }



                }));
            }
        }
    }
}
