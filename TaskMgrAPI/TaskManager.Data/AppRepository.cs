using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace TaskManager.Data
{
    public class AppRepository : IAppRepository
    {
        private static SqlProviderServices instance = SqlProviderServices.Instance;
        public TaskManagerCapsuleEntities _dbContext;
        public AppRepository()
        {
            _dbContext = new TaskManagerCapsuleEntities();
        }

        public bool CreateTask(Task task)
        {
            try
            {
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteTaskById(int taskId)
        {
            bool result = false;
            try
            {
                Task task = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool EndTaskById(int taskId)
        {
            bool result = false;
            try
            {
                Task task = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
                task.End_date = DateTime.Now.Date;
                _dbContext.Entry(task).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public int GetId()
        {
            return 1;
        }

        public List<ParentTask> GetParentTasks()
        {
            List<ParentTask> tasks = new List<ParentTask>();
            try
            {
                tasks = _dbContext.ParentTasks.ToList();
            }
            catch (Exception ex) { }
            return tasks;
        }

        public Task GetTaskById(int taskId)
        {
            Task task = new Task();
            try
            {
                task = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
            }
            catch (Exception ex) { }
            return task;
        }

        public List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            try
            {
                tasks = _dbContext.Tasks.ToList();
            }
            catch (Exception ex) { }
            return tasks;
        }

        public bool UpdateTask(Task task, int taskId)
        {
            bool result = false;
            try
            {
                Task taskFromDb = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
                taskFromDb.End_date = task.End_date;
                taskFromDb.Start_date = task.Start_date;
                taskFromDb.Parent_Id = task.Parent_Id;
                taskFromDb.Priority = task.Priority;
                taskFromDb.Task1 = task.Task1;
                _dbContext.Entry(taskFromDb).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
