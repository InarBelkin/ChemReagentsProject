using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL.Repository
{

    public class DbReposSQL : IDbRepos
    {
        private ChemContext db;
        private SuppliesRepSQL SupplyRepos;
        private ReagentsRepSQL ReagentsRepos;
        private ReportsRepSQL Report;
        private SupplierRepSQL Supplier;
        private SolutionRezipeRepSQL rezipe;
        private SolutRezLineRepSQl rezipeLine;
        private SolutionRepSQL solution;
        private SolutionLineRepSQL solutionLine;
        private ConcentracionsRepSQL concentrations;
        private ConsumptionsRepSQL consump;
        public DbReposSQL()
        {
            db = new ChemContext();
            //MessageBox.Show("adsf");
        }

        public IReportRepos Reports => Report ?? (Report = new ReportsRepSQL(db));

        public IRepository<Supply> Supplies => SupplyRepos ?? (SupplyRepos = new SuppliesRepSQL(db));

        public IRepository<Reagent> Reagents => ReagentsRepos ?? (ReagentsRepos = new ReagentsRepSQL(db));

        public IRepository<Supplier> Suppliers => Supplier ?? (Supplier = new SupplierRepSQL(db));

        public IRepository<Solution_recipe> Solution_Recipes => rezipe ?? (rezipe = new SolutionRezipeRepSQL(db));

        public IRepository<Solution_recipe_line> Solution_Rezipe_Line => rezipeLine ?? (rezipeLine = new SolutRezLineRepSQl(db));

        public IRepository<Solution> Solutions => solution ?? (solution = new SolutionRepSQL(db));

        public IRepository<Solution_line> Solution_Lines => solutionLine ?? (solutionLine = new SolutionLineRepSQL(db));

        public IRepository<Concentration> Concentrations => concentrations ?? (concentrations = new ConcentracionsRepSQL(db));

        public IRepository<Supply_consumption> Consumptions => consump ?? (consump = new ConsumptionsRepSQL(db));

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
