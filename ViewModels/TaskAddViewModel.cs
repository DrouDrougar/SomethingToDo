using SomethingToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomethingToDoApp.ViewModels
{
    public class TaskAddViewModel : BaseViewModel
    {
        public ObservableCollection<TaskModel> TaskCollection { get; set; }
        public TaskAddViewModel()
        {
            TaskCollection = new ObservableCollection<TaskModel>
            {
                new TaskModel()
                {
                    Id = 1,
                    TaskName = "Test",
                    TaskDescription = "Do a thing Test"
                }
            };
        }
    }
}
