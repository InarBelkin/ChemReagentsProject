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
        public ICrudRepos<SupplyM> Supplies
        {
            get
            {
                return supplies ?? (supplies = new SupplyCrud(db));
            }
        }

        private ReagentCrud reagents;
        public ICrudRepos<ReagentM> Reagents
        {
            get
            {
                return reagents?? (reagents = new ReagentCrud(db));
            }
        }

        public SupplierCrud suppliers;
        public ICrudRepos<SupplierM> Suppliers
        {
            get
            {
                return suppliers ?? (suppliers = new SupplierCrud(db));
            }
        }

        public bool Save()
        {
            //if (db.Save() > 0) return true;
            //else return false;
            return db.Save() > 0;
        }
    }
}
