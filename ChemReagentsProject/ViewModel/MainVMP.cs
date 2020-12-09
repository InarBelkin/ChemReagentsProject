using ChemReagentsProject.Interfaces;
using ChemReagentsProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.ViewModel
{
    partial class MainVM
    {
        public IMainWin MainWin;
        private PageReagents pReag;
        public PageReagents PReag
        {
            get
            {
                return pReag ?? (pReag = new PageReagents(dbOp, rep));
            }
            set
            {
                pReag = value;
            }
        }
        private PageSolutionRecipe pSolutRec;
        public PageSolutionRecipe PSolutRec
        {
            get
            {
                return pSolutRec ?? (pSolutRec = new PageSolutionRecipe());
            }
            set
            {
                pSolutRec = value;
            }
        }

    }
}
