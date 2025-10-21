
using SomethingToDoApp.Models;
using SomethingToDoApp.Sqlite;
using SomethingToDoApp.ViewModels;


namespace SomethingToDoApp;

public partial class TaskAddPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private int _editTaskId;
    public TaskAddPage(LocalDbService dbService)
	{
		InitializeComponent();
		BindingContext = new TaskAddViewModel();

        _dbService = dbService;

        Task.Run(async () => listView.ItemsSource = await _dbService.GetTasks());
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        string taskName = AddTaskNameField.Text;
        string taskDescription = AddTaskDescriptionField.Text;

        if (_editTaskId == 0)
        {
           await _dbService.CreateTask(new TaskModel
           {
               TaskName = taskName,
               TaskDescription = taskDescription,
           });
        }
        else
        {
            await _dbService.UpdateTask(new TaskModel
            {
                Id = _editTaskId,
                TaskName = taskName,
                TaskDescription = taskDescription,
            });

            _editTaskId = 0;
        }
        listView.ItemsSource = await _dbService.GetTasks();
    }

    private void ViewAllTasksButton_Clicked(object sender, EventArgs e)
    {
        if (listView.IsVisible == false)
        {
            listView.IsVisible = true;
        }
        else
        { listView.IsVisible = false; }   
    }

    private async void listView_ItemTapped_1(object sender, ItemTappedEventArgs e)
    {
        var tasks = (TaskModel)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
        switch (action)
        {
            case "Edit":
                _editTaskId = tasks.Id;
                AddTaskNameField.Text = tasks.TaskName;
                AddTaskDescriptionField.Text = tasks.TaskDescription;
                break;
            case "Delete":
                await _dbService.DeleteTask(tasks);
                listView.ItemsSource = await _dbService.GetTasks();
                break;
        }
    }
}