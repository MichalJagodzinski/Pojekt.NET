﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Core;

namespace TaskManager.Core
{
    public class TasksPageViewModel : BaseViewModel
    {
        public ObservableCollection<TaskViewModel> TasksList { get; set; } = new ObservableCollection<TaskViewModel>();

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskPriority { get; set; }
        public ICommand AddButtonCommand { get; set; }
        public ICommand DeleteButtonCommand { get; set; }

        public TasksPageViewModel()
        {
            AddButtonCommand = new Command(AddTask);
            DeleteButtonCommand = new Command(DeleteTask);
        }

        public void AddTask()
        {
            var task = new TaskViewModel
            {
                Title = TaskTitle,
                Description = TaskDescription,
                Priority = TaskPriority,
                DateTimeTaskCreated = DateTime.Now,
            };
            TasksList.Add(task);

            TaskTitle = string.Empty;
            TaskDescription = string.Empty;
            TaskPriority = string.Empty;
        }

        public void DeleteTask()
        {
            var checkedTasks = TasksList.Where(x => x.IsChecked).ToList();
            foreach (var task in checkedTasks)
            {
                TasksList.Remove(task);
            }
        }
    }
}
