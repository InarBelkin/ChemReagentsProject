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


    }
}
