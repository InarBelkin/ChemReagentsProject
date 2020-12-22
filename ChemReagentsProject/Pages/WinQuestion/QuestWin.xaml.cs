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

namespace ChemReagentsProject.Pages.WinQuestion
{
    /// <summary>
    /// Логика взаимодействия для QuestWin.xaml
    /// </summary>
    public partial class QuestWin : Window
    {
        public QuestWin( string str)
        {
            InitializeComponent();
            DataContext = new QuestVM(str);
        }
    }
}
