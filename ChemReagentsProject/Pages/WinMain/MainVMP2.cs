using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChemReagentsProject.ViewModel
{
    partial class MainVM
    {
        private void ExceptionSystem_ConnectLost(object sender, Exception e)
        {
            if(MessageBox.Show("Отсутствует подключение к серверу\nВозможно в будущем можно будет изменять в настройках строку подключения, а пока можно только выйти из программы","Ошибка",MessageBoxButton.YesNo )== MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
          
        }
    }
}
