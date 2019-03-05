using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.DTO
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public Nullable<int> Parent_Id { get; set; }
        public string Task { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public string ParentTask { get; set; }
        public bool IsTaskEnded
        {
            get
            {
                if (EndDate <= DateTime.Now.Date)
                    return true;
                else
                    return false;
            }
        }
    }
}
