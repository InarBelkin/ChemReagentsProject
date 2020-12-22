using ChemReagentsProject.Interfaces;
using ChemReagentsProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChemReagentsProject.Pages.PageReagents;

namespace ChemReagentsProject.ViewModel
{
    partial class MainVM
    {
        private PageReag pReag;
        public PageReag PReag
        {
            get
            {
                return pReag ?? (pReag = new PageReag(dbOp, rep));
            }
            set
            {
                pReag = value;
            }
        }
        //private PageSolutionRecipe pSolutRec;
        //public PageSolutionRecipe PSolutRec
        //{
        //    get
        //    {
        //        return pSolutRec ?? (pSolutRec = new PageSolutionRecipe());
        //    }
        //    set
        //    {
        //        pSolutRec = value;
        //    }
        //}

    }
}
