using AutoMapper;
using NBench;
using System;
using System.Web;
using TaskManager.Business;
using TaskManager.Business.DTO;
using TaskManager.Data;

[assembly: PreApplicationStartMethod(typeof(TaskManager.LoadTest.TaskManagerAPILoadTest), "Start")]
namespace TaskManager.LoadTest
{
    public class TaskManagerAPILoadTest
    {
        IAppRepository _appRepository;
        public TaskManagerAPILoadTest()
        {

        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            Mapper.Reset();
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
            _appRepository = new AppRepository();
        }

        [PerfBenchmark(Description = "--------NBench Result for GetAllTasks----------",
                                                     NumberOfIterations = 2,
                                                     RunMode = RunMode.Throughput,
                                                     TestMode = TestMode.Measurement, SkipWarmups = false)]

        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetAllTasks()
        {
            AppBusiness appBusiness = new AppBusiness(_appRepository);
            appBusiness.GetTasks();
        }

        [PerfBenchmark(Description = "--------NBench Result for Create task----------",
                                                        NumberOfIterations = 2,
                                                        RunMode = RunMode.Throughput,
                                                        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetAllParentsTasks()
        {
            AppBusiness appBusiness = new AppBusiness(_appRepository);
            appBusiness.GetParentTasks();
        }

        [PerfBenchmark(Description = "--------NBench Result for Create task----------",
                                                        NumberOfIterations = 2,
                                                        RunMode = RunMode.Throughput,
                                                        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetTaskById()
        {
            AppBusiness appBusiness = new AppBusiness(_appRepository);
            appBusiness.GetTaskById(1);
        }


        [PerfBenchmark(Description = "--------NBench Result for Create task----------",
                                                        NumberOfIterations = 2,
                                                        RunMode = RunMode.Throughput,
                                                        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void CreateTask()
        {
            TaskDTO taskDTO = new TaskDTO()
            {
                Task = "Load Test Task",
                Priority = 1,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddDays(1).Date
            };
            AppBusiness appBusiness = new AppBusiness(_appRepository);
            appBusiness.CreateTask(taskDTO);

        }

        [PerfBenchmark(Description = "--------NBench Result for Create task----------",
                                                        NumberOfIterations = 2,
                                                        RunMode = RunMode.Throughput,
                                                        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void UpdateTask()
        {
            TaskDTO taskDTO = new TaskDTO()
            {
                Task = "Load Test Update Task",
                Priority = 1,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddDays(1).Date,
                TaskId = 1
            };
            AppBusiness appBusiness = new AppBusiness(_appRepository);
            appBusiness.UpdateTask(taskDTO, 1);
        }

        [PerfBenchmark(Description = "--------NBench Result for Create task----------",
                                                        NumberOfIterations = 2,
                                                        RunMode = RunMode.Throughput,
                                                        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void EndTask()
        {
            AppBusiness appBusiness = new AppBusiness(_appRepository);
            appBusiness.EndTaskById(1);
        }



        [PerfCleanup]
        public void Cleanup()
        {
        }


    }

}
