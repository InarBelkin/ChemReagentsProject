using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class DbDataOperation : IDbCrud
    {
        IDbRepos db;

        public DbDataOperation(IDbRepos repos)
        {
            db = repos;
        }

        private SupplyCrud supplies;
        public ICrudRepos<SupplyM> Supplies => supplies ?? (supplies = new SupplyCrud(db));

        private ReagentCrud reagents;
        public ICrudRepos<ReagentM> Reagents => reagents ?? (reagents = new ReagentCrud(db));

        public SupplierCrud suppliers;
        public ICrudRepos<SupplierM> Suppliers => suppliers ?? (suppliers = new SupplierCrud(db));

        private Solution_RezipeCrud solutRecipes;
        public ICrudRepos<SolutionRezipeM> SolutRecipes => solutRecipes ?? (solutRecipes = new Solution_RezipeCrud(db));

        private Solution_Rez_LineCrud solutRecLines;
        public ICrudRepos<SolutRezLineM> SolutRecLines => solutRecLines ?? (solutRecLines = new Solution_Rez_LineCrud(db));

        public bool Save()
        {
            //if (db.Save() > 0) return true;
            //else return false;
            return db.Save() > 0;
        }
    }
}
