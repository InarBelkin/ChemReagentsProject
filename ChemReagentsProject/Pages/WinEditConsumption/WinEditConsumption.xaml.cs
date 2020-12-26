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

namespace ChemReagentsProject.Pages.WinEditConsumption
{
    /// <summary>
    /// Логика взаимодействия для WinEditConsumption.xaml
    /// </summary>
    public partial class WinEditConsumption : Window
    {
        public WinEditConsumption(IDbCrud cr, IReportServ report, ConsumptionM icons)
        {
            InitializeComponent();
            DataContext = new EditConsumptionVM(cr, report, icons);
        }
    }
}
