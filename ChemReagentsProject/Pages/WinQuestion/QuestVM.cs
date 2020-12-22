using ChemReagentsProject.Interfaces;
using ChemReagentsProject.NavService;
using ChemReagentsProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Pages.WinQuestion
{
    class QuestVM : IRecognizable
    {
        Guid ThisGuid;
        string text;
        public QuestVM( string str)
        {
            ThisGuid = Guid.NewGuid();
            text = str;
        }

        public string InText
        {
            get
            {
                return text;
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
