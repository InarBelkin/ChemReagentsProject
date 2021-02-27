using DAL.Interfaces;
using DAL.Tables;

namespace DAL.Repository
{
    public class ConsumptionsRepSQL : IReposAbstract<Supply_consumption>
    {
        public ConsumptionsRepSQL(ChemContext dbcontext) : base(dbcontext.Consumptions, dbcontext) { }

    }

    class ReagentsRepSQL : IReposAbstract<Reagent>
    {
        public ReagentsRepSQL(ChemContext dbcontext) : base(dbcontext.Reagents, dbcontext) { }
    }

    public class SolutionRepSQL : IReposAbstract<Solution>
    {
        public SolutionRepSQL(ChemContext dbcontext) : base(dbcontext.Solutions, dbcontext) { }
    }

    public class SolutionLineRepSQL : IReposAbstract<Solution_line>
    {
        public SolutionLineRepSQL(ChemContext dbcontext) : base(dbcontext.Solution_Lines, dbcontext) { }
    }

    public class SolutionRezipeRepSQL : IReposAbstract<Solution_recipe>
    {
        public SolutionRezipeRepSQL(ChemContext dbcontext) : base(dbcontext.Solution_Recipes, dbcontext) { }
    }

    public class SolutRezLineRepSQl : IReposAbstract<Solution_recipe_line>
    {
        public SolutRezLineRepSQl(ChemContext dbcontext) : base(dbcontext.Solution_Recipe_Lines, dbcontext) { }
    }

    public class ConcentracionsRepSQL : IReposAbstract<Concentration>
    {
        public ConcentracionsRepSQL(ChemContext dbcontext) : base(dbcontext.Concentration, dbcontext) { }
    }

    public class SupplierRepSQL : IReposAbstract<Supplier>
    {
        public SupplierRepSQL(ChemContext dbcontext) : base(dbcontext.Suppliers, dbcontext) { }
    }

    public class SuppliesRepSQL : IReposAbstract<Supply>
    {
        public SuppliesRepSQL(ChemContext dbcontext) : base(dbcontext.Supplies, dbcontext) { }
    }

    public class ReportsRRepSQL : IReposAbstract<Report>
    {
        public ReportsRRepSQL(ChemContext dbcontext) : base(dbcontext.Reports, dbcontext) { }
    }
}
