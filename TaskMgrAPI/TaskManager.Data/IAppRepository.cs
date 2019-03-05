using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public interface IAppRepository
    {
        int GetId();
        List<Task> GetTasks();
        Task GetTaskById(int taskId);
        bool UpdateTask(Task task, int taskId);
        bool DeleteTaskById(int taskId);
        bool EndTaskById(int taskId);
        List<ParentTask> GetParentTasks();
        bool CreateTask(Task task);
    }
}
