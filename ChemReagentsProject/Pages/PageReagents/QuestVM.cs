using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.PageReagents
{
    class QuestVM: IRecognizable
    {
        Guid ThisGuid;
        public QuestVM()
        {
            ThisGuid = Guid.NewGuid();
        }

        public string InText
        {
            get
            {
                return "Вы хотите удалить реагент, у которого есть поставки, вы уверены?";
            }
        }

        private RelayCommand okB;
        public RelayCommand OkB
        {
            get
            {
                return okB ?? (okB = new RelayCommand(obj =>
                {
                    WindowService.CloseWindow(ThisGuid, true);
                }));
            }
        }
        private RelayCommand cancB;
        public RelayCommand CancB
        {
            get
            {
                return cancB ?? (cancB = new RelayCommand(obj =>
                {
                    WindowService.CloseWindow(ThisGuid, false);
                }));
            }
        }

        public Guid GetGuid()
        {
            return ThisGuid;
        }
    }
}
