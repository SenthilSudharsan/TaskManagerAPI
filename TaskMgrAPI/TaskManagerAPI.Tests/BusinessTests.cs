using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TaskManager.Business;
using TaskManager.Business.DTO;
using TaskManager.Data;

namespace TaskManagerAPI.Tests
{
    [TestFixture]
    public class BusinessTests
    {

        Mock<IAppRepository> mock = new Mock<IAppRepository>();

        public BusinessTests()
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
        }

        [Test]
        public void get_all_task_from_repo()
        {
            mock.Setup(a => a.GetTasks()).Returns(new List<Task> { new Task { Task_Id = 1, Task1 = "TestTask", Priority = 1, Start_date = DateTime.Now.Date } });
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            List<TaskDTO> result = appBusiness.GetTasks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TestTask", result.ElementAt(0).Task);
        }

        [Test]
        public void Get_All_Parent_Tasks_from_Repo()
        {
            // Arrange

            mock.Setup(a => a.GetParentTasks()).Returns(new List<ParentTask> { new ParentTask { Parent_Id = 1, Parent_Task = "Test parent Task" } });
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            IEnumerable<ParentTaskDTO> result = appBusiness.GetParentTasks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test parent Task", result.ElementAt(0).Parent_Task);
        }

        [Test]
        public void Get_Task_By_Id_from_repo()
        {
            // Arrange
            mock.Setup(a => a.GetTaskById(1)).Returns(new Task { Task_Id = 1, Task1 = "TestTask", Priority = 1, Start_date = DateTime.Now.Date });
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            TaskDTO result = appBusiness.GetTaskById(1);

            // Assert
            Assert.AreEqual("TestTask", result.Task);
        }

        [Test]
        public void Create_a_task_from_Repo()
        {
            // Arrange
            mock.Setup(a => a.CreateTask(It.IsAny<Task>())).Returns(true);
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            var result = appBusiness.CreateTask(new TaskDTO());

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Update_the_task_from_repo()
        {
            // Arrange
            mock.Setup(a => a.UpdateTask(It.IsAny<Task>(), 1)).Returns(true);
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            var result = appBusiness.UpdateTask(new TaskDTO { TaskId = 1, Task = "TestTask", Priority = 1, StartDate = DateTime.Now.Date }, 1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void End_the_task_from_repo()
        {
            // Arrange
            mock.Setup(a => a.EndTaskById(1)).Returns(true);
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            var result = appBusiness.EndTaskById(1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Delete_the_task_from_repo()
        {
            // Arrange
            mock.Setup(a => a.DeleteTaskById(1)).Returns(true);
            AppBusiness appBusiness = new AppBusiness(mock.Object);

            // Act
            var result = appBusiness.DeleteTaskById(1);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
