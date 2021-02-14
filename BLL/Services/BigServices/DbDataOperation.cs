using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BigServices
{
    public class DbDataOperation : IDBCrud
    {
        IDbRepos db;
        public DbDataOperation(IDbRepos repos)
        {
            db = repos;
            // DAL.Additional.ExceptionSystemD.ConnectLost += ExceptionSystemD_ConnectLost;
        }

        private ReagentCrud reagents;
        public ICrudRepos<ReagentM> Reagents => reagents ?? (reagents = new ReagentCrud(db));
        private SupplyCrud supplies;
        public ICrudRepos<SupplyM> Supplies => supplies ?? (supplies = new SupplyCrud(db));
        private SupplierCrud suppliers;
        public ICrudRepos<SupplierM> Suppliers => suppliers ?? (suppliers = new SupplierCrud(db));
        private RecipeCrud recipes;
        public ICrudRepos<RecipeM> Recipes => recipes ?? (recipes = new RecipeCrud(db));
        private ConcentrationCrud concentrations;
        public ICrudRepos<ConcentrationM> Concentrations => concentrations ?? (concentrations = new ConcentrationCrud(db));
        private RecipeLineCrud recipelines;
        public ICrudRepos<RecipeLineM> Recipe_Lines => recipelines ??(recipelines = new RecipeLineCrud(db));
    }
}
