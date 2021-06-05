using System.Net.NetworkInformation;
using System.Windows.Controls;
using BLL.Interfaces;
using ChemReagentsProject.VVM.Additional;
using ChemReagentsProject.VVM.Reagent;

namespace ChemReagentsProject.VVM.MainWin
{
    public class MainWinVM : BaseVM
    {
        //private IUnitOfWork db;
        public MainWinVM()
        {
            //db = IoC.IoC.Get<IUnitOfWork>();
            CurrentPage = new MainTabMenu();
        }
        private UserControl currentPage;
        public UserControl CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }
    }
}