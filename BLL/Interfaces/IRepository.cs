using System.Collections.Generic;
using DAL.Tables;

namespace BLL.Interfaces
{
    public interface IRepository<T> where T : BaseDBModel
    {
        public T GetItem(int id);
        public List<T> GetList();
        public void Create(T item);
        public void Update(T item);
        public bool Delete(int id);
    }
}