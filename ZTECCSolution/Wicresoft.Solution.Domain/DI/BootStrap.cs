using StructureMap;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Wicresoft.Solution.Domain
{
    public class BootStrap
    {
        //public static void ApiServiceStartUp()
        //{
        //    var container = (IContainer)Ioc.Initialize(new ApiControllerRegistry());
        //    GlobalConfiguration.Configuration.DependencyResolver = new SmDependencyResolver(container);

        //}

        public static void MvcControllerStartUp()
        {
            var container = (IContainer)Ioc.Initialize(new ControllerRegistry());
            GlobalConfiguration.Configuration.DependencyResolver = new SmDependencyResolver(container);
            DependencyResolver.SetResolver(
                t =>
                {
                    try { return ObjectFactory.GetInstance(t); }
                    catch { return null; }
                },
                t => ObjectFactory.GetAllInstances<object>().Where(s => s.GetType() == t)
            );
        }
    }
}
