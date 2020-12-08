using BLL.Interfaces;
using BLL.Util;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.Util;
using ChemReagentsProject.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChemReagentsProject   //может пойму когда-нибудь?
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWin
    {
        public MainWindow()//ну что
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("DBConnection"));
            IDbCrud crudServ = kernel.Get<IDbCrud>();
            
            MainVM a = new MainVM( this, crudServ);
            DataContext = a;
        }

        public void ChangePage(Page p)
        {
            FrameRight.Navigate(p);
        }
    }
}
