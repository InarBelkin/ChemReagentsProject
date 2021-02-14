using BLL.Interfaces;
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

namespace ChemReagents.Pages.SolutionsPage
{
    /// <summary>
    /// Логика взаимодействия для PageSolutions.xaml
    /// </summary>
    public partial class PageSolutions : UserControl
    {
        public PageSolutions(IDBCrud cr, IReportServ report)
        {
            InitializeComponent();
            DataContext = new SolutionVM(cr, report);
        }

        private void ReagComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
