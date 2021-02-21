using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces            //уровень костылей зашкаливает
{                                   // зачем оно так сделано?
    public abstract class IModel<T>  :IIModel where T:new()      //да просто основная программа не знает о DLL, из которой берётся этот класс T
    {
        /// <summary>
        /// Модель копирует свойтва из dal-модели(вместо конструктора)
        /// </summary>
        internal abstract void setfromDal(T item);
        internal virtual T getDal()
        {
            T d = new T();
            updDal(d);
            return d;
        }
        internal abstract void updDal(T item);


    }

    public abstract class IIModel : INotifyPropertyChanged      
    {
        protected int id;
        public int Id
        {
            get => id;
            set => id = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual bool Validate()
        {
            return true;
        }
    
    }
}
