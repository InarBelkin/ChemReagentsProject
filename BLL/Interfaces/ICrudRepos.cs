using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrudRepos<T>
    {
        ObservableCollection<T> GetList();
        T GetItem(int id);
        Exception Create(T item);
        Exception Update(T item);
        void Delete(int id);
    }
}
  