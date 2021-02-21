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
using System.Windows.Shapes;

namespace ChemReagents.Pages.SolutionsPage
{
    /// <summary>
    /// Логика взаимодействия для WinSupplEditRec.xaml
    /// </summary>
    public partial class WinSupplEditRec : Window
    {
        public WinSupplEditRec(IDBCrud cr, IReportServ report, SolutionM solut)
        {
            InitializeComponent();
            DataContext = new SupplEditRecVM(cr, report, solut);
            ReagentColumn.ItemsSource = cr.Reagents.GetList();
            SupplyColumn.ItemsSource = cr.Supplies.GetList();
        }

     
    }
}
