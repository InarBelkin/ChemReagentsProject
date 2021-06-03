using Ninject;
using Ninject.Parameters;

namespace ChemReagentsProject.IoC
{
    public static class IoC
    {
        private static StandardKernel Kernel = new StandardKernel(new NinjectRegistrations());

        public static T Get<T>(params IParameter[] parameters)
        {
            return Kernel.Get<T>(parameters);
        }
    }
}