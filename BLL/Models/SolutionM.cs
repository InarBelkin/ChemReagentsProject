﻿using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BLL.Models
{
    public class SolutionM : INotifyPropertyChanged
    {
        private int? soltuionRecipeId;
        private int? concentrationId;
        private DateTime date_Begin;

        public int Id { get; set; }
        public int? SolutionRecipeId
        {
            get => soltuionRecipeId;
            set
            {
                soltuionRecipeId = value;
                OnPropertyChanged();
            }
        }
        public int? ConcentrationId
        {
            get => concentrationId;
            set
            {
                concentrationId = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date_Begin
        {
            get => date_Begin;
            set
            {
                date_Begin = value;
                OnPropertyChanged();
            }
        }

        private string concentrName;
        public string ConcentrName
        {
            get => concentrName;
            set
            {
                concentrName = value;
            }
        }

        private string recipeName;
        public string RecipeName
        {
            get => recipeName;
            set
            {
                recipeName = value;
            }
        }

        public SolutionM() { }
        public SolutionM(Solution s)
        {
            Id = s.Id;
            ConcentrationId = s.ConcentrationId;
            // SolutionRecipeId = s.SolutionRecipeId;
            Date_Begin = s.Date_Begin;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Solution getDal()
        {
            Solution s = new Solution();
            s.Id = Id;
            s.ConcentrationId = ConcentrationId;
            s.Date_Begin = Date_Begin;
            return s;
        }

        internal void updDal(Solution s)
        {
            s.Id = Id;
            s.ConcentrationId = ConcentrationId;
            s.Date_Begin = Date_Begin;
        }


    }

    public class SolutionLineM : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private int solutionId;
        public int SolutionId { get => solutionId; set { solutionId = value; OnPropertyChanged(); } }
        private int? supplyId;
        public int? SupplyId { get => supplyId; set { supplyId = value; OnPropertyChanged(); } }
        private string nameOtherComp;
        public string NameOtherComp { get => nameOtherComp; set { nameOtherComp = value; OnPropertyChanged(); } }

        public Visibility VisibComb { get; set; }
        public Visibility VisibNoComb { get; set; }
        private float count;
        public float Count { get => count; set { count = value; OnPropertyChanged(); } }

        private ObservableCollection<ReagentM> reagList;
        public ObservableCollection<ReagentM> ReagList
        {
            get => reagList;
            set
            {
                reagList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SupplyM> supplyList;
        public ObservableCollection<SupplyM> SupplyList
        {
            get => supplyList;
            set
            {
                supplyList = value;
                OnPropertyChanged();
            }
        }


        public SolutionLineM() { }
        public SolutionLineM(Solution_line sl)
        {
            Id = sl.Id;


            SolutionId = sl.SolutionId;
            SupplyId = sl.SupplyId;
            NameOtherComp = sl.NameOtherComponent;
            Count = sl.Count;
            if (SupplyId == null)
            {
                VisibComb = Visibility.Collapsed;
                VisibNoComb = Visibility.Visible;
            }
            else
            {
                VisibComb = Visibility.Visible;
                VisibNoComb = Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        internal Solution_line getDal()
        {
            Solution_line l = new Solution_line();
            l.Id = Id;
            l.SolutionId = SolutionId;
            l.SupplyId = l.SupplyId;
            l.Count = Count;
            l.NameOtherComponent = NameOtherComp;
            return l;
        }

        internal void updDal(Solution_line l)
        {
            l.Id = Id;
            l.SolutionId = SolutionId;
            l.SupplyId = l.SupplyId;
            l.NameOtherComponent = NameOtherComp;
            l.Count = Count;
        }
    }
}
