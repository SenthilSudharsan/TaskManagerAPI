using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using TaskManager.Business;
using TaskManager.Business.DTO;

namespace TaskManagerAPI.Controllers
{
    [RoutePrefix("Task")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        IAppBusiness _appBusiness;

        public TaskController(IAppBusiness appBusiness)
        {
            _appBusiness = appBusiness;
        }

        [Route("get")]
        public IEnumerable<TaskDTO> Get()
        {
            return _appBusiness.GetTasks();
        }

        [Route("get/{id}")]
        public TaskDTO Get(int id)
        {
            return _appBusiness.GetTaskById(id);
        }

        // POST api/values
        [Route("create")]
        public void Post([FromBody]TaskDTO value)
        {
            _appBusiness.CreateTask(value);
        }

        // PUT api/values/5

        [Route("update")]
        [HttpPost]
        public void update([FromBody]TaskDTO value)
        {
            _appBusiness.UpdateTask(value, value.TaskId);
        }

        // DELETE api/values/5
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            _appBusiness.DeleteTaskById(id);
        }

        // END api/values/end/5
        [Route("end/{id}")]
        public void End(int id)
        {
            _appBusiness.EndTaskById(id);
        }

        // GETPARENT api/values/parents/hello world
        [Route("parents")]
        public IEnumerable<ParentTaskDTO> GetParents()
        {
            return _appBusiness.GetParentTasks();
        }
    }
}
