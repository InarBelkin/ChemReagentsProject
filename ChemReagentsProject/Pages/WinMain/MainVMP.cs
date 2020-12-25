using ChemReagentsProject.Interfaces;
using ChemReagentsProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChemReagentsProject.Pages.PageReagents;
using ChemReagentsProject.Pages.PageReziepe;
using ChemReagentsProject.Pages.PageSolutions;

namespace ChemReagentsProject.ViewModel
{
    partial class MainVM
    {
        private void ChangePage()
        {
            if (pSolution != null)
            {
                pSolution.Dispose();
                pSolution = null;
            }
        }

        private PageReag pReag;
        public PageReag PReag
        {
            get => (new PageReag(dbOp, rep));
            //set => pReag = value;
        }

        private PageReziepe pReziepe;
        public PageReziepe PReziepe
        {
            get =>  new PageReziepe(dbOp, rep);
           //set => pReziepe = value;
        }

        private PageSolution pSolution;
        public PageSolution PSolution
        {
            get
            {
                pSolution = new PageSolution(dbOp, rep);
                return pSolution;
            }
        }
    }
}
