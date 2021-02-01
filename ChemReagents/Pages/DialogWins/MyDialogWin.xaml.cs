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
    /// Логика взаимодействия для MyDialogWin.xaml
    /// </summary>
    public partial class MyDialogWin : Window
    {
        public MyDialogWin(String TextQuest)
        {
            InitializeComponent();
            TextBl.Text = TextQuest;
        }

        private void YesB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void NoB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
