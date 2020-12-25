using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
namespace ChemReagentsProject.NavService
{
    static class InarService    
    {
        public static event EventHandler ChangeClick;

        public static void ClickChangeInv()
        {
            
            ChangeClick?.Invoke(null, null);
        }


        public static event EventHandler<ReagentM> ReagentChange;
        public static void ReagentChangeInv(ReagentM r)
        {
            ReagentChange?.Invoke(null,r);
        }

        public static event EventHandler<SupplyM> SupplyChange;
        public static void SupplyChangeInv(SupplyM s)
        {
            SupplyChange?.Invoke(null, s);
        }


        public static event EventHandler<string> SolLineTextChange;
        public static void SolLineTextChangeInv(string s)
        {
            SolLineTextChange?.Invoke(null,s);
        }

    }
}
