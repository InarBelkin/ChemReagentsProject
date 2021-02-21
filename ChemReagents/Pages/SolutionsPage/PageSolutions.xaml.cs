using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ChemReagents.Pages.SolutionsPage
{
    /// <summary>
    /// Логика взаимодействия для PageSolutions.xaml
    /// </summary>
    public partial class PageSolutions : UserControl, IPageSolution
    {
        public PageSolutions(IDBCrud cr, IReportServ report)
        {
            InitializeComponent();
            DataContext = new SolutionVM(cr, report, this);
        }





        private void ChangeRec_Click(object sender, RoutedEventArgs e)
        {
            ChangeRecipeButton.Invoke(null, e);
        }
        public event EventHandler ChangeRecipeButton;

        private void ReagComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count>0)
            {
                SelectReagentChanged.Invoke(null, e.AddedItems[0] as ReagentM);
            }
        }
        public event EventHandler<ReagentM> SelectReagentChanged;

        private void SupplComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count>0)
            {
                SelectSupplyChanged.Invoke(null, e.AddedItems[0] as SupplyM);
            }
        }
        public event EventHandler<SupplyM> SelectSupplyChanged;

        private void SupplText_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameOtherCompChanged?.Invoke(null, (sender as TextBox).Text);
        }
        public event EventHandler<string> NameOtherCompChanged;

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

        private void DateBegin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as DatePicker;
            var b = a.SelectedDate;
            var c = (DateTime)b;
            DateBeginChanged.Invoke(null,c);
        }

        private void DateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateEndCHanged.Invoke(null, (DateTime)(sender as DatePicker).SelectedDate);
        }

        public event EventHandler<DateTime> DateBeginChanged;
        public event EventHandler<DateTime> DateEndCHanged;

    }


}
