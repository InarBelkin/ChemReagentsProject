using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.PageSolutions
{
    class WinEditRecInSupplVM : INotifyPropertyChanged, IRecognizable
    {
        Guid ThisGuid;
        IDbCrud dbOp;
        IReportServ rep;
        SolutionM tempsolut;
        SolutionM EditSolut;
        public WinEditRecInSupplVM (IDbCrud cr, IReportServ report, SolutionM solut)
        {
            ThisGuid = Guid.NewGuid();
            dbOp = cr;
            rep = report;
            EditSolut = solut;
            if(solut.ConcentrationId!=null)
            {
                tempsolut = new SolutionM();
                tempsolut.ConcentrationId = solut.ConcentrationId;
                ConcentrationM c = dbOp.Concentrations.GetItem((int)solut.ConcentrationId);
                SolutionRezipeM sr = dbOp.SolutRecipes.GetItem(c.SolutionRecipeId);
                tempsolut.SolutionRecipeId = sr.Id;

            }
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

        public ObservableCollection<SolutionRezipeM> ListRecip
        {
            get
            {
                ObservableCollection<SolutionRezipeM> a = dbOp.SolutRecipes.GetList();
                return a;
            }
        }

        private SolutionRezipeM selectRecip;
        public SolutionRezipeM SelectRecip
        {
            get
            {
                return selectRecip;
            }
            set
            {
                selectRecip = value;
                tempsolut.SolutionRecipeId = selectRecip.Id;
                if(selectRecip!=null)
                {
                    ListConcent = rep.ConcentrbyRecipe(selectRecip.Id);
                    if(ListConcent.Count!=0)
                    {
                        SelectConcent = ListConcent[0];
                        OnPropertyChanged("SelectConcent");
                    }
                }
            }
        }

        private ObservableCollection<ConcentrationM> listConcent;
        public ObservableCollection<ConcentrationM> ListConcent
        {
            get
            {
                return listConcent;
            }
            set
            {
                listConcent = value;
                OnPropertyChanged("ListConcent");
            }
        }

        private ConcentrationM selectConcent;
        public ConcentrationM SelectConcent
        {
            get => selectConcent;
            set
            {
                selectConcent = value;
                if(selectConcent!=null)
                {
                    tempsolut.ConcentrationId = selectConcent.Id;
                    ListRecLine = rep.GetRecipeLine(selectConcent.Id);
                }
                else
                {
                    tempsolut.ConcentrationId = null;
                    ListRecLine = new ObservableCollection<SolutRezLineM>();
                }

                
            }
        }

        private ObservableCollection<SolutRezLineM> listRecLine;
        public ObservableCollection<SolutRezLineM> ListRecLine
        {
            get => listRecLine;
            set
            {
                listRecLine = value;
                OnPropertyChanged("ListRecLine");
            }
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
                        case "Save":    //возможно надо добавить проверку, если рецепт есть, а концентрации нет - обе null
                            EditSolut.ConcentrationId = tempsolut.ConcentrationId;
                            EditSolut.SolutionRecipeId = tempsolut.SolutionRecipeId;
                            WindowService.CloseWindow(ThisGuid, true);
                            break;
                        case "Cancel":
                            WindowService.CloseWindow(ThisGuid, false);
                            break;
                    }



                }));
            }
        }



    }
}
