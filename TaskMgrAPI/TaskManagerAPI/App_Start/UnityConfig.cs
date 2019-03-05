using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TaskManager.Business;
using TaskManager.Data;
using TaskManagerAPI.Controllers;
using Unity;

namespace TaskManagerAPI
{
    public class UnityConfig
    {
        public static void RegisterContainers(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IAppRepository, AppRepository>();
            container.RegisterType<IAppBusiness, AppBusiness>();
            container.Resolve<AppBusiness>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}