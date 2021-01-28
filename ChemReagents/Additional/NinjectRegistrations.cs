using BLL.Interfaces;
using BLL.Services.BigServices;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Additional
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IDBCrud>().To<DbDataOperation>();
            Bind<IReportServ>().To<ReportService>();
        }
    }
}
