using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using TaskManager.Data;

namespace TaskManagerAPI.Tests
{
    [TestFixture]
    public class DataTests
    {

        Mock<TaskManagerCapsuleEntities> mock = new Mock<TaskManagerCapsuleEntities>();
        Task task;
        public DataTests()
        {
            AppRepository appRepository = new AppRepository();
            appRepository.CreateTask(new Task() { Task1 = "TestTask", Priority = 1, Start_date = DateTime.Now.Date, End_date = DateTime.Now.Date.AddDays(1) });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask").FirstOrDefault();
        }

        [Test]
        public void Create_a_task_in_db()
        {
            // Arrange
            AppRepository appRepository = new AppRepository();

            // Act
            var result = appRepository.CreateTask(new Task() { Task1 = "TestTask2", Priority = 1, Start_date = DateTime.Now.Date, End_date = DateTime.Now.Date.AddDays(1) });

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Get_all_task_Fom_Db()
        {

            mock.Setup(x => x.Tasks.Add(It.IsAny<Task>())).Returns((Task u) => u);
            AppRepository appRepository = new AppRepository();

            var result = appRepository.GetTasks();

            Assert.IsNotNull(result);
        }


        [Test]
        public void Get_All_Parent_Tasks_in_db()
        {
            // Arrange

            AppRepository appRepository = new AppRepository();

            // Act
            var result = appRepository.GetParentTasks();

            // Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void Get_Task_By_Id_in_db()
        {
            // Arrange
            AppRepository appRepository = new AppRepository();

            // Act
            var result = appRepository.GetTaskById(task.Task_Id);

            // Assert
            Assert.NotNull(result);
        }



        [Test]
        public void Update_the_task_in_db()
        {
            // Arrange
            AppRepository appRepository = new AppRepository();
            appRepository.CreateTask(new Task() { Task1 = "TestTask1", Priority = 1, Start_date = DateTime.Now.Date, End_date = DateTime.Now.Date.AddDays(1) });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask1").FirstOrDefault();
            // Act
            var result = appRepository.UpdateTask(task, task.Task_Id);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void End_the_task_in_db()
        {
            // Arrange
            AppRepository appRepository = new AppRepository();
            appRepository.CreateTask(new Task() { Task1 = "TestTask2", Priority = 1, Start_date = DateTime.Now.Date, End_date = DateTime.Now.Date.AddDays(1) });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask2").FirstOrDefault();
            // Act
            var result = appRepository.EndTaskById(task.Task_Id);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Delete_the_task_in_db()
        {
            // Arrange
            AppRepository appRepository = new AppRepository();
            appRepository.CreateTask(new Task() { Task1 = "TestTask3", Priority = 1, Start_date = DateTime.Now.Date, End_date = DateTime.Now.Date.AddDays(1) });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask3").FirstOrDefault();
            // Act
            var result = appRepository.DeleteTaskById(task.Task_Id);

            // Assert
            Assert.AreEqual(true, result);
        }
    }


}
