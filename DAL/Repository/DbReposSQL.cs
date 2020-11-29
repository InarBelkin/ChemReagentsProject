﻿using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    
    public class DbReposSQL : IDbRepos
    {
        private ChemContext db;
        private SuppliesRepSQL SupplyRepos;
        private ReagentsRepSQL ReagentsRepos;
        public DbReposSQL()
        {
            db = new ChemContext();
        }

        public IRepository<Supply> Supplies
        {
            get
            {
                return SupplyRepos ?? (SupplyRepos = new SuppliesRepSQL(db));
            }
        }

        public IRepository<Reagent> Reagents
        {
            get
            {
                return ReagentsRepos ?? (ReagentsRepos = new ReagentsRepSQL(db));
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
