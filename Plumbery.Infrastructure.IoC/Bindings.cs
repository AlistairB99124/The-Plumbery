using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;
using Plumbery.Domain.Interfaces.Infrastructure;
using Plumbery.Infrastructure.Data.Configuration;
using Plumbery.Domain.Interfaces.Repositories;
using Plumbery.Infrastructure.Data.Repositories;
using Plumbery.Domain.Interfaces.Domain;
using Plumbery.Domain.Services;

namespace Plumbery.Infrastructure.IoC
{
    public class Bindings
    {
        public static void Start(Container container) {

            //Infrastructure
            container.Register<IRepositoryManager, RepositoryManager>();
            container.Register<IUnitOfWork, UnitOfWork>();
            container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>), Lifestyle.Scoped);
            container.Register(typeof(IUserRepository), typeof(UserRepository), Lifestyle.Scoped);
            container.Register(typeof(ITimeSheetRepository), typeof(TimeSheetRepository), Lifestyle.Scoped);
            container.Register(typeof(IInventoryRepository), typeof(InventoryRepository), Lifestyle.Scoped);
            container.Register(typeof(IPlumberRepository), typeof(PlumberRepository), Lifestyle.Scoped);
            container.Register(typeof(ISupervisorRepository), typeof(SupervisorRepository), Lifestyle.Scoped);
            container.Register(typeof(IHomeRepository), typeof(HomeRepository), Lifestyle.Scoped);
            container.Register(typeof(ISiteRepository), typeof(SiteRepository), Lifestyle.Scoped);
            container.Register(typeof(IWarehouseRepository), typeof(WarehouseRepository), Lifestyle.Scoped);

            // Domain
            container.Register(typeof(IUserService), typeof(UserService), Lifestyle.Scoped);
            container.Register(typeof(ITimeSheetService), typeof(TimeSheetService), Lifestyle.Scoped);
            container.Register(typeof(IInventoryService), typeof(InventoryService), Lifestyle.Scoped);
            container.Register(typeof(IPlumberService), typeof(PlumberService), Lifestyle.Scoped);
            container.Register(typeof(ISupervisorService), typeof(SupervisorService), Lifestyle.Scoped);
            container.Register(typeof(IHomeService), typeof(HomeService), Lifestyle.Scoped);
            container.Register(typeof(ISiteService), typeof(SiteService), Lifestyle.Scoped);
            container.Register(typeof(IWarehouseService), typeof(WarehouseService), Lifestyle.Scoped);

            // Service Locator
            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocatorAdapter(container));
        }
    }
}
