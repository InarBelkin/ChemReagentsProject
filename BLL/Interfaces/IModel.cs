using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces            //уровень костылей зашкаливает
{                                   // зачем оно так сделано?
    public abstract class IModel<T>  :IIModel      //да просто основная программа не знает о DLL, из которой берётся этот класс T
    {
        internal abstract T getDal();
        internal abstract void updDal(T item);
        /// <summary>
        /// Модель копирует свойтва из dal-модели(вместо конструктора)
        /// </summary>
        internal abstract void setfromDal(T item);
    }

    public abstract class IIModel : INotifyPropertyChanged      
    {
        protected int id;
        public int Id
        {
            get => id;
            protected set => id = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
