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
    /// Логика взаимодействия для WinSupply.xaml
    /// </summary>
    public partial class WinSupply : Window
    {
        public WinSupply(IDBCrud cr, IReportServ report, SupplyM suppl)
        {
            InitializeComponent();
            DataContext = new SuppliesWinVM(cr, report, suppl);

            box.KeyDown += Box_KeyDown;
        }

        private void Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BindingExpression bind = box.GetBindingExpression(TextBox.TextProperty);
                bind.UpdateSource();
            }
        }
    }
}
