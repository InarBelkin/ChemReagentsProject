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
using ChemReagentsProject.NavService;

namespace ChemReagentsProject  
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //IMainWin
    {
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
           
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("DBConnection"));
            IDbCrud crudServ = kernel.Get<IDbCrud>();
            IReportServ RepServ = kernel.Get<IReportServ>();
            
            MainVM a = new MainVM( crudServ,RepServ);
           // Navigation.AddNav(this, FrameRight.NavigationService);
            DataContext = a;

            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.AddNav(this, FrameRight.NavigationService);
            Navigation.LoadDoneInv(this);
        }



        //public void ChangePage(Page p)
        //{
        //    FrameRight.Navigate(p);
        //}
    }
}
