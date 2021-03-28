using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ChemReagents.Pages.ReciepePage
{
    /// <summary>
    /// Логика взаимодействия для PageReziepe.xaml
    /// </summary>
    public partial class PageReziepe : UserControl, IPageRecipe
    {
        public PageReziepe(IDBCrud cr, IReportServ report)
        {
            InitializeComponent();
            DataContext = new ReziepeVM(cr, report, this);

        }

        public event EventHandler<ReagentM> ChangeReagent;

        public void SetReagents(ObservableCollection<ReagentM> reagents)
        {
            // CReagents.ItemsSource = reagents;

        }

        private void ReagComb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox CB = sender as ComboBox;
            
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                string oldText = tb.Text;
                CB.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
                CB.Text = oldText;
                tb.Text = oldText;
                // tb.SelectionStart = 1;
                //tb.SelectedText = "";
                tb.Select(oldText.Length, 0);
                
            }
            
            if (tb.SelectionStart == 0 && CB.SelectedItem == null)
            {
                CB.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }
            CB.IsDropDownOpen = true;
            if (tb.SelectionStart == 0 && CB.SelectedItem != null)
                CB.IsDropDownOpen = false;

            if (CB.SelectedItem == null)
            {
                // Если элемент не выбран менять фильтр
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB.ItemsSource);
                cv.Filter = s => ((s as ReagentM).Name).IndexOf(CB.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }


        private void ReagComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeReagent.Invoke(sender, e.AddedItems[0] as ReagentM);
        }

        public void SetPlusLocation(int Marg)
        {
            //double heigh = ReactiveDG.ColumnHeaderHeight + Marg * ReactiveDG.RowHeight;
            //if (double.IsNaN(heigh))
            //    
            //else PlusButton.Margin = new Thickness(0, heigh, 0, 0);
            double heigh = 30 + 30 * Marg;
            PlusButton.Margin = new Thickness(0, heigh, 0, 0);
        }
    }
}
