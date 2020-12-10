using BLL.Interfaces;
using ChemReagentsProject.Interfaces;
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
using System.Windows.Shapes;

namespace ChemReagentsProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для WinEditSupplies.xaml
    /// </summary>
    public partial class WinEditSupplies : Window, IEditSupplWin
    {
        public WinEditSupplies(IDbCrud cr, IReportServ report, int SupplyId)
        {
            InitializeComponent();
            DataContext = new EditSuppliesVM(this,cr, report, SupplyId);
            //CalendB.SelectedDate = DateTime.Now;
            //CalendB.DisplayDate = DateTime.Now;
           
        }

        public void DialogRez(bool rez)
        {
            DialogResult = rez;
        }
    }
}
