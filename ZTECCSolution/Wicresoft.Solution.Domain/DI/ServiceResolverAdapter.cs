using Wicresoft.Solution.Utils;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Wicresoft.Solution.Domain
{
    /// <summary>
    /// IOC初始化方法
    /// </summary>
    public static class Ioc
    {
        private static IContainer _container;
        public static IContainer Initialize(Registry registry)
        {
            if (_container != null)
            {
                return _container;
            }
            var container = ObjectFactory.Container;
            container.Configure(x =>
            {
                x.AddRegistry(registry);
            });
            _container = container;
            return container;
        }
    }
    /// <summary>
    /// StructureMap 服务类
    /// </summary>
    public class StructureMapScope : IDependencyScope
    {
        protected IContainer Container;
        public StructureMapScope(IContainer container)
        {
            Container = container;
        }
        public void Dispose()
        {
            Container = null;
        }
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }
            try
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                    return Container.TryGetInstance(serviceType);

                return Container.GetInstance(serviceType);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
        }
    }

    /// <summary>
    /// 解决方案
    /// </summary>
    public class SmDependencyResolver : StructureMapScope, IDependencyResolver
    {
        private IContainer _container;
        public SmDependencyResolver(IContainer container)
            : base(container)
        {
            _container = container;
        }
        public IDependencyScope BeginScope()
        {
            return new StructureMapScope(_container);
        }
    }
}
