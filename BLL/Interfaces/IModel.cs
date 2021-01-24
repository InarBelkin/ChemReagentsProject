using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IModel<T>
    {
        int Id { get; set; }
        T getDal();
        void updDal(T item);
    }
}
