using BLL.Interfaces;
using BLL.Models;
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

namespace ChemReagents.Pages.ReagentsPage
{
    /// <summary>
    /// Логика взаимодействия для PageReag.xaml
    /// </summary>
    public partial class PageReag : UserControl, IPageReag
    {
        public PageReag(IDBCrud cr, IReportServ report)
        {
            InitializeComponent();
            DataContext = new ReagentVM(cr, report, this);
            
        }

   

        public void setDevMode(bool Mode)
        {
            if(Mode)
            {
                IdColumn.Visibility = Visibility.Visible;
            }
            else
            {
                IdColumn.Visibility = Visibility.Hidden;
            }
          
        }
    }
}
