using DAL.Interfaces;
using DAL.Tables;

namespace DAL.Repository
{
    class ReagentsRepSQL : IReposAbstract<Reagent>
    {
        // private ChemContext db;

        public ReagentsRepSQL(ChemContext dbcontext) : base(dbcontext.Reagents, dbcontext)
        {
            db = dbcontext;
        }

    }
}
