﻿using BLL.Interfaces;
using BLL.Util;
using ChemReagentsProject.Interfaces;
using ChemReagentsProject.Util;
using ChemReagentsProject.ViewModel;
using Ninject;
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

namespace ChemReagentsProject  
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWin
    {
        public MainWindow()
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("DBConnection"));
            IDbCrud crudServ = kernel.Get<IDbCrud>();
            IReportServ RepServ = kernel.Get<IReportServ>();
            
            MainVM a = new MainVM( this, crudServ,RepServ);
            DataContext = a;
        }

        public void ChangePage(Page p)
        {
            FrameRight.Navigate(p);
        }
    }
}
