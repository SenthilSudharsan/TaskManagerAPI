using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaskManager.Business.DTO;
using TaskManager.Data;

namespace TaskManagerAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.RegisterContainers);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskDTO, TaskManager.Data.Task>().ForMember(dest => dest.Task1, opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.Task_Id, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.Start_date, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.End_date, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Parent_Id, opt => opt.MapFrom(src => src.Parent_Id)); 
                cfg.CreateMap<ParentTask, ParentTaskDTO>();
                cfg.CreateMap<TaskManager.Data.Task, TaskDTO>()
                .ForMember(dest => dest.Task, opt => opt.MapFrom(src => src.Task1))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Task_Id))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Start_date))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.End_date))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Parent_Id, opt => opt.MapFrom(src => src.Parent_Id));
            });
        }
    }
}
