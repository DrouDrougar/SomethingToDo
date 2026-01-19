
using SomethingToDoApp.Models;
using SomethingToDoApp.Sqlite;
using SomethingToDoApp.ViewModels;
using SQLite;
using System.Data.Common;


namespace SomethingToDoApp;

public partial class TaskAddPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private int _editTaskId;
    private readonly SQLiteAsyncConnection _connection;
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

        AddTaskDescriptionField.Text = string.Empty;
        AddTaskNameField.Text = string.Empty;
        listView.ItemsSource = await _dbService.GetTasks();
    }

    private void ViewAllTasksButton_Clicked(object sender, EventArgs e)
    {
        if (listView.IsVisible == false)
        {
            listView.IsVisible = true;
            ViewAllTasksButton.Text = "Hide All Tasks";
            GenerateRandomTasks.IsVisible = true;
        }
        else
        {
            listView.IsVisible = false;
            ViewAllTasksButton.Text = "View All Tasks";
            GenerateRandomTasks.IsVisible = false;
        }
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

    private async void GenerateRandomTasks_Clicked(object sender, EventArgs e)
    {

        List<TaskModel> tasks = new List<TaskModel>
        {
            //new TaskModel { TaskName = "Clean Sleeping Quarters", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean Bathroom", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean Kitchen", TaskDescription = "" },
            //new TaskModel { TaskName = "Take out Trash", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean LivingRoom", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean Sleeping Quarters", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean Bathroom", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean Kitchen", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean Hobby Room", TaskDescription = "" },
            //new TaskModel { TaskName = "Clean LivingRoom", TaskDescription = "" },
            new TaskModel { TaskName = "Clean Sleeping Quarters 1", TaskDescription = "Dust all surfaces, change bed linens, and vacuum the floor to maintain a fresh and tidy sleeping environment." },
            new TaskModel { TaskName = "Clean Bathroom 1", TaskDescription = "Scrub the sink, toilet, and shower; mop the floor; and restock toiletries for a hygienic bathroom." },
            new TaskModel { TaskName = "Clean Kitchen 1", TaskDescription = "Wipe down countertops, clean appliances, wash dishes, and mop the floor to ensure a sanitary cooking space." },
            new TaskModel { TaskName = "Take out Trash 1", TaskDescription = "Collect all household waste, replace trash bags, and dispose of recyclables properly." },
            new TaskModel { TaskName = "Clean LivingRoom 1", TaskDescription = "Dust furniture, vacuum carpets and upholstery, and organize magazines and remote controls." },
            new TaskModel { TaskName = "Clean Sleeping Quarters 2", TaskDescription = "Organize closets, wipe mirrors, and air out the room to create a comfortable resting area." },
            new TaskModel { TaskName = "Clean Bathroom 2", TaskDescription = "Clean mirrors and fixtures, disinfect high-touch areas, and empty bathroom bins." },
            new TaskModel { TaskName = "Clean Kitchen 2", TaskDescription = "Clean inside the refrigerator, wipe cabinet doors, and sanitize cutting boards." },
            new TaskModel { TaskName = "Clean Hobby Room 2", TaskDescription = "Organize supplies, dust shelves, and clean work surfaces to maintain a productive hobby space." },
            new TaskModel { TaskName = "Clean LivingRoom 2", TaskDescription = "Polish wooden furniture, fluff cushions, and tidy up electronic devices and cables." },
            new TaskModel { TaskName = "Vacuum Carpets", TaskDescription = "Thoroughly vacuum all carpeted areas to remove dust, dirt, and allergens." },
            new TaskModel { TaskName = "Wash Windows", TaskDescription = "Clean all windows inside and out to improve natural light and visibility." },
            new TaskModel { TaskName = "Dust Blinds and Curtains", TaskDescription = "Remove dust and allergens from window treatments to maintain air quality." },
            new TaskModel { TaskName = "Clean Refrigerator", TaskDescription = "Empty, clean shelves and drawers, and discard expired food items." },
            new TaskModel { TaskName = "Mop Floors", TaskDescription = "Mop all hard surface floors to remove stains and maintain cleanliness." },
            new TaskModel { TaskName = "Organize Pantry", TaskDescription = "Sort and arrange pantry items, discard expired goods, and clean shelves." },
            new TaskModel { TaskName = "Clean Oven", TaskDescription = "Remove grease and burnt food residues from the oven interior." },
            new TaskModel { TaskName = "Water Indoor Plants", TaskDescription = "Water and care for all indoor plants to keep them healthy and vibrant." },
            new TaskModel { TaskName = "Clean Light Fixtures", TaskDescription = "Dust and wipe down all light fixtures to improve brightness and appearance." },
            new TaskModel { TaskName = "Laundry", TaskDescription = "Wash, dry, fold, and put away clothes and linens." },
        };

        foreach (var task in tasks)
        {
            //if(task.TaskName != )
            await _dbService.CreateTask(new TaskModel
            {
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
            });
        }
        listView.ItemsSource = await _dbService.GetTasks();
    }
}