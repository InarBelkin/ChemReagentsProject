using BLL.Models;
using BLL.Services.BigServices;
using DAL.Interfaces;
using DAL.Tables;

namespace BLL.Services
{
    class ReagentCrud : IServCrudAbstr<ReagentM, Reagent>
    {
        public ReagentCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Reagents) { }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании реактива произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать реактив";
                case 2: return "При изменении реактива произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить реактив";
            }
            return "Что-то странное происходит";
        }
    }
}
