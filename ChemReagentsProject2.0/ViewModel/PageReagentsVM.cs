using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.ViewModel
{
    class PageReagentsVM : INotifyPropertyChanged
    {
        private RelayCommand mycommand;
        public RelayCommand MyCommand
        {
            get
            {
                return mycommand ?? (mycommand = new RelayCommand(obj =>
                {
                    MessageBox.Show("Забло");
                    //ChemContext db = new ChemContext();
                    //db.Reagents.Add(new Reagent() { Id = 0, Name = "asdf", units = "bb" });
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
