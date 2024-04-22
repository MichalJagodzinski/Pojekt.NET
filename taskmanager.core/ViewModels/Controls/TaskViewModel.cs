using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core;

namespace TaskManager.Core
{
    public class TaskViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }

        public DateTime DateTimeTaskCreated { get; set; }
    }
}
