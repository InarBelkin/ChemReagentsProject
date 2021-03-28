﻿using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BLL.Models
{
    public class SolutionLineM : IModel<Solution_line>
    {
        private int solutionId;
        public int SolutionId { get => solutionId; set { solutionId = value; OnPropertyChanged(); } }
        private int reagentId;
        public int ReagentId { get => reagentId; set { reagentId = value; visibComb = reagentId > 0; OnPropertyChanged(); OnPropertyChanged("VisibComb"); OnPropertyChanged("VisibNoComb"); } }
        private int? supplyId;
        public int? SupplyId { get => supplyId; set { supplyId = value; OnPropertyChanged(); } }

        private ObservableCollection<SupplyM> supplyList;
        public ObservableCollection<SupplyM> SupplyList { get => supplyList; set { supplyList = value; OnPropertyChanged(); } }

        private string nameOtherComponent;
        public string NameOtherComponent { get => nameOtherComponent; set { nameOtherComponent = value; OnPropertyChanged(); } }

        private decimal count;
        public decimal Count { get => count; set { count = value; OnPropertyChanged(); OnPropertyChanged("CanTake"); } }
        private decimal countBalance;
        public decimal CountBalance { get => countBalance; set { countBalance = value; OnPropertyChanged(); OnPropertyChanged("CanTake"); } }
        public bool CanTake { get => Count <= CountBalance || SupplyId == null; }
        public bool CanTakeRec { get => Count <= CountBalance && SupplyId != null; }
     

        private bool visibComb;
        public Visibility VisibComb { get => (visibComb ? Visibility.Visible : Visibility.Hidden); }
        public Visibility VisibNoComb { get => (visibComb ? Visibility.Hidden : Visibility.Visible); }
        private string units;
        public string Units { get => units; set { units = value;OnPropertyChanged(); } }
        public SolutionLineM() { }
        public SolutionLineM(Solution_line s) { setfromDal(s); }
        internal override void setfromDal(Solution_line item)
        {
            Id = item.Id;
            SolutionId = item.SolutionId;
            SupplyId = item.SupplyId;
            NameOtherComponent = item.NameOtherComponent;
            Count = item.Count;

        }

        internal override void updDal(Solution_line item)
        {
            item.Id = Id;
            item.SolutionId = SolutionId;
            item.SupplyId = SupplyId;
            item.NameOtherComponent = NameOtherComponent;
            if(Count<=CountBalance || SupplyId == null)
            {
                item.Count = Count;
            }
           
        }
    }
}