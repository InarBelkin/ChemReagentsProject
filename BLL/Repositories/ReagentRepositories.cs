using BLL.Interfaces;
using BLL.Repositories.BigRepositories;
using DAL.Context;
using DAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repositories
{
    public class ReagentRep: AbstractRepository<Reagent>
    {
        public ReagentRep(ChemContext db) : base(db, db.Reagents)
        {
        }
    }
}