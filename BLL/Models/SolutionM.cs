﻿using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SolutionM 
    {
        public int Id { get; set; }
        public int? SolutionRecipeId { get; set; }
        public DateTime Date_Begin { get; set; }

        public SolutionM() { }
        public SolutionM(Solution s)
        {
            Id = s.Id;
            SolutionRecipeId = s.SolutionRecipeId;
            Date_Begin = s.Date_Begin;
        }

        internal Solution getDal()
        {
            Solution s = new Solution();
            s.Id = Id;
            s.SolutionRecipeId = SolutionRecipeId;
            s.Date_Begin = Date_Begin;
            return s;
        }

        internal void updDal(Solution s)
        {
            s.Id = Id;
            s.SolutionRecipeId = SolutionRecipeId;
            s.Date_Begin = Date_Begin;
        }

      
    }

    public class SolutionLineM
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public int SupplyId { get; set; }
        public float Count { get; set; }

        public SolutionLineM() { }
        public SolutionLineM(Solution_line sl)
        {
            Id = sl.Id;
            SolutionId = sl.SolutionId;
            SupplyId = sl.SupplyId;
            Count = sl.Count;
        }

        internal Solution_line getDal()
        {
            Solution_line l = new Solution_line();
            l.Id = Id;
            l.SolutionId = SolutionId;
            l.SupplyId = l.SupplyId;
            l.Count = Count;
            return l;
        }

        internal void updDal(Solution_line l)
        {
            l.Id = Id;
            l.SolutionId = SolutionId;
            l.SupplyId = l.SupplyId;
            l.Count = Count;
        }
    }
}