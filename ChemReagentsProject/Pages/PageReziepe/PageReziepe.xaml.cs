﻿using BLL.Interfaces;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChemReagentsProject.Pages.PageReziepe
{
    /// <summary>
    /// Логика взаимодействия для PageReziepe.xaml
    /// </summary>
    public partial class PageReziepe : UserControl
    {
        public PageReziepe(IDbCrud cr, IReportServ report)
        {
            DataContext = new ReziepeVM(cr, report);
            InitializeComponent();
           
            //column.ItemsSource = cr.Reagents.GetList();
        }
    }
}
