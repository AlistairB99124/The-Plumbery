using Plumbery.Infrastructure.IoC;
using Plumbery.UI.MVC.App_Start;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(SimpleInjectorInitialiser), "Initialise")]
namespace Plumbery.UI.MVC.App_Start {
    /// <summary>
    /// Class to initialise simple injector
    /// </summary>
    public class SimpleInjectorInitialiser {
        /// <summary>
        /// Initialise Simple Injector
        /// </summary>
        public static void Initialise() {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Calling the Simple Injector modules
            InitialiseContainer(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
        /// <summary>
        /// Run Registration Process in IoC Project
        /// </summary>
        /// <param name="container">Simple Injector Container</param>
        private static void InitialiseContainer(Container container) {
            Bindings.Start(container);
        }
    }
}
