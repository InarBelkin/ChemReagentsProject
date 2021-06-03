using BLL.Interfaces;
using BLL.Repositories.BigRepositories;
using Ninject.Modules;

namespace ChemReagentsProject.IoC
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWorkSQL>().WithConstructorArgument("SQLConnection");
        }
    }
}