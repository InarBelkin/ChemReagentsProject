using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ChemReagentsProject.Pages.PageSolutions
{
    /// <summary>
    /// Логика взаимодействия для PageSolution.xaml
    /// </summary>
    public partial class PageSolution : UserControl
    {
        public PageSolution(IDbCrud cr, IReportServ report)
        {
            InitializeComponent();

            DataContext = new SolutionVM(cr, report);

            SolutionsDG.Visibility = Visibility.Visible;

        }

        private void ChangeRec_Click(object sender, RoutedEventArgs e)
        {
            InarService.ClickChangeInv();
        }

        private void ReagComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InarService.ReagentChangeInv(e.AddedItems[0] as ReagentM);
        }
    }
}
