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

namespace ChemReagents.Pages.SupplyWin
{
    /// <summary>
    /// Логика взаимодействия для WinConsump.xaml
    /// </summary>
    public partial class WinConsump : Window
    {
        public WinConsump(IDBCrud cr, IReportServ report, SupplyStingM s)
        {
            InitializeComponent();
            DataContext = new EditConsumptionVM(cr, report, s);
        }
    }
}
