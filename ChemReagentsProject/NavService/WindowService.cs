using ChemReagentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.NavService
{
    static class WindowService
    {
        public static void CloseWindow(Guid ID, bool Result)
        {
            foreach (Window Item in Application.Current.Windows)
            {
                IRecognizable ViewModel = Item.DataContext as IRecognizable;
                if (ViewModel != null && ViewModel.GetGuid() == ID)
                {
                    Item.DialogResult = Result;
                    Item.Close();
                    break;  //если убрать возможно будет ошибка, проверить
                }
            }
        }
    }
}
