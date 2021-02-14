using BLL.Models;
using BLL.Services.BigServices;
using BLL.Services.FIlters;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BLL.Services
{
    class SolutionCrud : IServCrudAbstr<SolutionM, Solution>
    {
        public SolutionCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Solutions) { }
        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании раствора произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать раствор";
                case 2: return "При изменении раствора произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить раствор";
            }
            return "Что-то странное происходит";
        }
    }
    class SolutionLineCrud : IServCrudAbstr<SolutionLineM, Solution_line>
    {
        public SolutionLineCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Solution_Lines) { }
        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании строки раствора произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать строку раствора";
                case 2: return "При изменении строки раствора произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить строку раствора";
            }
            return "Что-то странное происходит";
        }
    }
}
