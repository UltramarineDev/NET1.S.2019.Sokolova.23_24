using Ninject;
using DAL.Interface.Repository;
using BLL.ServiceImplementation;
using Logger;
using Logger.Interface;
using BLL.Interface.Services;
using BLL;
using DAL.EntityFramework.Concrete;

namespace DependencyResolver
{
    public static class ResolverModule
    {
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            kernel.Bind<IAccountRepository>().To<DAL.Fake.AccountRepository>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IBonuseCalculator>().To<BonusCalculator>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<ILog>().To<NLogger>();
        }
    }
}
