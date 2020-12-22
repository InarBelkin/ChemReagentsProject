using BLL.Interfaces;
using BLL.Models;
using ChemReagentsProject.Interfaces;
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

namespace ChemReagentsProject.Pages.PageReziepe
{
    /// <summary>
    /// Логика взаимодействия для PageReziepe.xaml
    /// </summary>
    public partial class PageReziepe : UserControl, IPageRezipe
    {
        public PageReziepe(IDbCrud cr, IReportServ report)
        {
           
            InitializeComponent();
            DataContext = new ReziepeVM(cr, report, this);
            // ReactiveDG.IsSynchronizedWithCurrentItem; //проверить

            //column.ItemsSource = cr.Reagents.GetList();
        }

        public void SetItemSource(ObservableCollection<ReagentM> PageList)
        {

            column.ItemsSource = PageList ;
        }
    }
}
