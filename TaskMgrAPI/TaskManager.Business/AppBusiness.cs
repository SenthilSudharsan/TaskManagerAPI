using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.DTO;
using TaskManager.Data;

namespace TaskManager.Business
{
    public class AppBusiness : IAppBusiness
    {
        IAppRepository _appRepository;

        public AppBusiness(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public bool CreateTask(TaskDTO task)
        {
            Data.Task Tasks = Mapper.Map<Data.Task>(task);
            return _appRepository.CreateTask(Tasks);
        }

        public bool DeleteTaskById(int taskId)
        {
            return _appRepository.DeleteTaskById(taskId);
        }

        public bool EndTaskById(int taskId)
        {
            return _appRepository.EndTaskById(taskId);
        }

        public List<ParentTaskDTO> GetParentTasks()
        {
            List<ParentTask> parentTasks = _appRepository.GetParentTasks();
            List<ParentTaskDTO> parentDTOTasks = Mapper.Map<List<ParentTaskDTO>>(parentTasks);
            return parentDTOTasks;

        }

        public TaskDTO GetTaskById(int taskId)
        {
            Data.Task tasks = _appRepository.GetTaskById(taskId);
            TaskDTO DTOTasks = Mapper.Map<TaskDTO>(tasks);
            List<ParentTaskDTO> parentTaskDTOs = GetParentTasks();
            if (parentTaskDTOs.Count > 0)
            {
                if (DTOTasks.Parent_Id != null)
                {
                    DTOTasks.ParentTask = parentTaskDTOs.Where(a => a.Parent_Id == DTOTasks.Parent_Id).Select(a => a.Parent_Task).FirstOrDefault();
                }
            }
            return DTOTasks;
        }

        public List<TaskDTO> GetTasks()
        {
            List<Data.Task> tasks = _appRepository.GetTasks();
            List<TaskDTO> DTOTasks = Mapper.Map<List<TaskDTO>>(tasks);
            List<ParentTaskDTO> parentTaskDTOs = GetParentTasks();
            if (parentTaskDTOs.Count > 0)
            {
                foreach (var item in DTOTasks)
                {
                    if (item.Parent_Id != null)
                    {
                        item.ParentTask = parentTaskDTOs.Where(a => a.Parent_Id == item.Parent_Id).Select(a => a.Parent_Task).FirstOrDefault();
                    }
                }
            }
            return DTOTasks;
        }

        public bool UpdateTask(TaskDTO task, int taskId)
        {
            Data.Task Tasks = Mapper.Map<Data.Task>(task);
            return _appRepository.UpdateTask(Tasks, taskId);
        }
    }
}
