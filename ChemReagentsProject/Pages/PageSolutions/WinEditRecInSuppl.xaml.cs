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

namespace ChemReagentsProject.Pages.PageSolutions
{
    /// <summary>
    /// Логика взаимодействия для WinEditRecInSuppl.xaml
    /// </summary>
    public partial class WinEditRecInSuppl : Window
    {
        public WinEditRecInSuppl(IDbCrud cr, IReportServ report, SolutionM solut)
        {
            InitializeComponent();
            DataContext = new WinEditRecInSupplVM(cr, report, solut);
        }
    }
}
