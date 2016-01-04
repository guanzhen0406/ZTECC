using Wicresoft.Solution.BLL;
using Wicresoft.Solution.IBLL;
using StructureMap.Configuration.DSL;

namespace Wicresoft.Solution.Domain
{

    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            For<IScreenAdapter>().Use<ScreenAdapter>();
            For<ISessionRepository>().Use<SessionRepository>();
            For<ICookieRepository>().Use<CookieRepository>();
            For<IUserService>().Use<UserService>();
        }
    }
}
