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
        public SupplyCrud Supplies => supplies ?? (supplies = new SupplyCrud(db));

        private ReagentCrud reagents;
        public ReagentCrud Reagents => reagents ?? (reagents = new ReagentCrud(db));

        public SupplierCrud suppliers;
        public SupplierCrud Suppliers => suppliers ?? (suppliers = new SupplierCrud(db));

        private Solution_RezipeCrud solutRecipes;
        public Solution_RezipeCrud SolutRecipes => solutRecipes ?? (solutRecipes = new Solution_RezipeCrud(db));

        private Solution_Rez_LineCrud solutRecLines;
        public Solution_Rez_LineCrud SolutRecLines => solutRecLines ?? (solutRecLines = new Solution_Rez_LineCrud(db));

        private SolutionCrud solutions;
        public SolutionCrud Solutions => solutions ?? (solutions = new SolutionCrud(db));

        private SolutionLineCrud solutLines;
        public SolutionLineCrud SolutLines => solutLines ?? (solutLines = new SolutionLineCrud(db));

        private ConcentrationCrud concentration;
        public ConcentrationCrud Concentrations => concentration ?? (concentration = new ConcentrationCrud(db));

        private ConsumptionCrud consump;
        public ConsumptionCrud Consumptions => consump ?? (consump = new ConsumptionCrud(db));

        public bool Save()
        {
            //if (db.Save() > 0) return true;
            //else return false;
            return db.Save() > 0;
        }
    }
}
