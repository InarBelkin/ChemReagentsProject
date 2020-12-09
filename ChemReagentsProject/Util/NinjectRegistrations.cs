using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
namespace ChemReagentsProject.Util
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbCrud>().To<DbDataOperation>();
             Bind<IReportServ>().To<ReportService>();
        }
    }
}
