using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagents.Additional
{
   static class WindowService
    {
        public static void CloseWindow(object ID, bool Result)
        {
            foreach (Window Item in Application.Current.Windows)
            {
                object ViewModel = Item.DataContext as object;
                if (ViewModel != null && ViewModel == ID)
                {
                    Item.DialogResult = Result;
                    Item.Close();
                    break;  //если убрать возможно будет ошибка, проверить
                }
            }
        }
    }
}
