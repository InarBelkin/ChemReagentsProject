using System.Windows.Controls;
using ChemReagentsProject.VVM.Additional;
using ChemReagentsProject.VVM.ReagentP;
using ChemReagentsProject.VVM.Recipes;
namespace ChemReagentsProject.VVM.MainWin
{
    public class MainTabMenuVM : BaseVM
    {
        public MainTabMenuVM()
        {
            CurrentPage = new ReagentPage();
        }

        private ContentControl _currentPage;

        public ContentControl CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand selectPageComm;

        public RelayCommand SelectPageComm => selectPageComm ??= new RelayCommand(obj =>
            {
                switch (obj as string)
                {
                    case "Reagent":
                        CurrentPage = new ReagentPage();
                        break;
                    case "Recipe":
                        CurrentPage = new RecipePage();
                        break;
                }
            }
        );
    }
}