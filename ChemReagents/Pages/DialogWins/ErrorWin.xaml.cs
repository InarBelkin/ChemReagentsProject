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

namespace ChemReagents.Pages.DialogWins
{
    /// <summary>
    /// Логика взаимодействия для ErrorWin.xaml
    /// </summary>
    public partial class ErrorWin : Window
    {
        public ErrorWin(Exception ex)
        {
            InitializeComponent();
            TextRus.Text = ex.Message;
            TextEng.Text = ex.InnerException.Message;
            TextEng2.Text = ex.InnerException.InnerException.Message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
