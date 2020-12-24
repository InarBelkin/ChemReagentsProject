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
    public partial class PageSolution : UserControl, IPageSolution
    {
        public PageSolution(IDbCrud cr, IReportServ report)
        {
            InitializeComponent();
            
            DataContext = new SolutionVM(cr, report, this);
          

        }

        public void SetConcentrations(ObservableCollection<ConcentrationM> concentrations)
        {
            //ConcClmn.ItemsSource = concentrations;

        }

        public void SetRecipes(ObservableCollection<SolutionRezipeM> recipes)
        {
            //RecipClmn.ItemsSource = recipes;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // var a = e.AddedItems;
            if (e.AddedItems.Count != 0)
            {
                InarService.ChangeSelectInv(e.AddedItems[0] as SolutionRezipeM);
            }

        }

        private void Concentr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count!=0)
            {
                InarService.ChangeConcent(e.AddedItems[0] as ConcentrationM);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SolutionVM).close = true;
        }
    }
}
