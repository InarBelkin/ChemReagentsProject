using DAL.Tables;

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Reagent> Reagents { get; }

        public int Save();
    }
}