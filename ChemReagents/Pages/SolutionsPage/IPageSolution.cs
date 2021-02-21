using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.SolutionsPage
{
    interface IPageSolution
    {
        event EventHandler ChangeRecipeButton;
        event EventHandler<ReagentM> SelectReagentChanged;
        event EventHandler<SupplyM> SelectSupplyChanged;
        event EventHandler<string> NameOtherCompChanged;
        event EventHandler<DateTime> DateBeginChanged;
        event EventHandler<DateTime> DateEndCHanged;
    }
}
