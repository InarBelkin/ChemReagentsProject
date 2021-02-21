using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReportRepos
    {
        void LoadAll();
        void DelSolutLines(int SolutId);
    }
}
