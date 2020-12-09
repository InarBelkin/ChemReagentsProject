using BLL.Interfaces;
using ChemReagentsProject.ViewModel;
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

namespace ChemReagentsProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageReagents.xaml
    /// </summary>
    public partial class PageReagents : Page
    {
        public PageReagents(IDbCrud cr, IReportServ report)
        {
            InitializeComponent();
            DataContext = new ReagentsVM(cr,report);
            
            //this.ShortSupplDG.ItemsSource = cr.Supplies.GetList();
            //this.ReagentGrid.ItemsSource = cr.Reagents.GetList();
        }






    }
}
