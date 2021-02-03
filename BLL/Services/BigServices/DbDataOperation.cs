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
        public ICrudRepos<SupplierM> Suppliers  => suppliers ?? (suppliers = new SupplierCrud(db));
    }
}
