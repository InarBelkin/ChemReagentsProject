
using ChemReagentsProject.Pages;
using System.ComponentModel;
using System.Windows.Controls;

namespace ChemReagentsProject.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                            RightPage = new PageReagents();
                            break;
                        case "Reziepe":
                            RightPage = new PageSolutionRezipe();
                            break;
                        default:
                            break;
                    }
                    //MessageBox.Show(obj as string);
                }));
            }
        }

        private Page rightPage;
        public Page RightPage
        {
            get
            {
                return rightPage;
            }
            set
            {
                rightPage = value;
                OnPropertyChanged("RightPage");
            }
        }

    }
}
