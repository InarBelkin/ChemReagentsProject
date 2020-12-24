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
        public static event EventHandler<SolutionRezipeM> ChangeSelectRez;
        public static void ChangeSelectInv(SolutionRezipeM s)
        {
            ChangeSelectRez?.Invoke(null, s);
        }

        public static event EventHandler<ConcentrationM> ChangeSelectConcentr;
        public static void ChangeConcent(ConcentrationM c)
        {
            ChangeSelectConcentr?.Invoke(null, c);
        }


        public static event EventHandler<bool> closesolutEv;

        public static void closeSolut()
        {
            closesolutEv?.Invoke(null,true);
        }


        public static event EventHandler ChangeClick;

        public static void ClickChangeInv()
        {
            ChangeClick?.Invoke(null,null);
        }


    }
}
