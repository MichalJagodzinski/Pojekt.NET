using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TaskManager.Core;
using Xceed.Wpf.Toolkit;


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
        public ICommand SaveButtonCommand { get; set; }
        public ICommand LoadButtonCommand { get; set; }
        public ICommand DeleteAllButtonCommand { get; set; }
        public string filePath = "C:\\Tasks.txt";

        public TasksPageViewModel()
        {
            AddButtonCommand = new Command(AddTask);
            DeleteButtonCommand = new Command(DeleteTask);
            SaveButtonCommand = new Command(SaveTasksToFile);
            LoadButtonCommand = new Command(LoadTasksFromFile);
            DeleteAllButtonCommand = new Command(DeleteAllTasksFromList);
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

        public void SaveTasksToFile()
        {
            StreamWriter writer = null;
            
            try
            {
                writer = new StreamWriter("C:\\SavedTasks.txt");
                foreach (var task in TasksList)
                {
                    writer.WriteLine($"Nazwa: {task.Title}, Opis: {task.Description}, Priorytet: {task.Priority}, Data utworzenia: {task.DateTimeTaskCreated}");
                }
                
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }

        public void LoadTasksFromFile()
        {
            using (StreamReader reader = new StreamReader("C:\\TasksToLoad.txt"))
            {
                try
                {
                    string data;
                    while ((data = reader.ReadLine()) != null)
                    {
                        string[] parts = data.Split(new[] { ',' }, 4, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 4)
                        {
                            string title = parts[0].Trim();
                            string description = parts[1].Trim();
                            string priority = parts[2].Trim();
                            TasksList.Add(new TaskViewModel
                            {
                                Title = title,
                                Description = description,
                                Priority = priority,
                                DateTimeTaskCreated = DateTime.Now,
                            });
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format danych");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteAllTasksFromList()
        {
            TasksList.Clear();
        }
    }
}
