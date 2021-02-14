using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.AdditionalWins.SettingsWin
{
    static class Settings
    {
        static private SettingsUnstat objSettings;
        static private SettingsUnstat ObjSettings { get { return objSettings ?? (objSettings = new SettingsUnstat()); } set => objSettings = value; }
        static public bool DevMode { get => ObjSettings.DevMode; set=> ObjSettings.DevMode = value; }

        static public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("CatSettings.dat", FileMode.OpenOrCreate);
            formatter.Serialize(fs, objSettings);
            fs.Close();
        }
        static public void Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("CatSettings.dat", FileMode.OpenOrCreate);
            if (fs.Length != 0)
            {
                SettingsUnstat s = (SettingsUnstat)formatter.Deserialize(fs);
                ObjSettings = s;
            }
            else ObjSettings = new SettingsUnstat();
         
           
        }
    }

    [Serializable]
    class SettingsUnstat
    {
        public SettingsUnstat()
        {
            DevMode = true;
        }
        public bool DevMode { get; set; }
    }
}
