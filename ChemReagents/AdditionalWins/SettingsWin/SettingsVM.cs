using ChemReagents.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemReagents.AdditionalWins.SettingsWin
{
    class SettingsVM
    {
        public bool EnableDevMode { get => Settings.DevMode; set { Settings.DevMode = value; } }


        private RelayCommand saveComm;
        public RelayCommand SaveComm
        {
            get
            {
                return saveComm ?? (saveComm = new RelayCommand(obj =>
                {
                    Settings.Save();
                })); 
            }
        }
    }
}
