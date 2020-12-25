using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL.Models;
namespace ChemReagentsProject.NavService
{
    static class InarService
    {
        private static event EventHandler changeClick;
        public static event EventHandler ChangeClick    // изменить рецепт
        {
            add
            {
                changeClick = value;
            }
            remove
            {
                changeClick = null;
            }
        }

        public static void ClickChangeInv()
        {
            changeClick?.Invoke(null, null);
        }

        private static event EventHandler<ReagentM> reagentChange;
        public static event EventHandler<ReagentM> ReagentChange { add {  reagentChange = value; } remove { reagentChange = null; } }
        public static void ReagentChangeInv(ReagentM r)
        {
            //var v = reagentChange.GetInvocationList().Length;
            //MessageBox.Show(v.ToString());
            reagentChange?.Invoke(null, r);
        }

        private static event EventHandler<SupplyM> supplyChange;
        public static event EventHandler<SupplyM> SupplyChange { add {  supplyChange = value; } remove { supplyChange = null; } }
        public static void SupplyChangeInv(SupplyM s)
        {
            supplyChange?.Invoke(null, s);
        }


        private static event EventHandler<string> solLineTextChange;
        public static event EventHandler<string> SolLineTextChange { add {  solLineTextChange = value; } remove { solLineTextChange = null; } }
        public static void SolLineTextChangeInv(string s)
        {
            solLineTextChange?.Invoke(null, s);
        }

    }
}
