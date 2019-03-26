using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PostalovTeam.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PostalovTeam
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new SimpleInjector.Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<PostalovTeamDbContext, PostalovTeamDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<AppUser>>(() => new UserStore<AppUser>(container.GetInstance<PostalovTeamDbContext>()), Lifestyle.Scoped);
            container.Register(() => new UserManager<AppUser>(container.GetInstance<IUserStore<AppUser>>()), Lifestyle.Scoped);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
