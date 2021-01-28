using BLL.Additional;
using BLL.Interfaces;
using ChemReagents.Additional;
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
using System.Windows.Shapes;

namespace ChemReagents.Pages.MainWin
{
    /// <summary>
    /// Логика взаимодействия для MainWin.xaml
    /// </summary>
    public partial class MainWin : Window, IMainWin
    {
        public MainWin()
        {
            InitializeComponent();
            //ContContr.Content = new ReagentsPage.PageReag();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("DBConnection"));
            IDBCrud crudServ = kernel.Get<IDBCrud>();
            IReportServ RepServ = kernel.Get<IReportServ>();

            DataContext = new MainVM( crudServ, RepServ, this);
        }

        public void SetPage(UserControl page)
        {
           // ContContr.DataContext = page;
           ContContr.Content = page;
        }
    }
}
