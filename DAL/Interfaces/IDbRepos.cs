using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepos
    {
        IRepository<Supply> Supplies { get; }
        IRepository<Reagent> Reagents { get; }
        IReportRepos Reports { get; }
        int Save();
    }
}
